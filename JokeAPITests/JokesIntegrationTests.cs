using System.Net;
using System.Net.Http.Json;
using System.Text;
using JokeAPI.DTO;
using JokeAPI.Model;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace JokeAPITests;

public class JokesIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public JokesIntegrationTests(WebApplicationFactory<Program> factory)
    {
        var options = new DbContextOptionsBuilder<JokeContext>().UseInMemoryDatabase("TestJokeDatabase").Options;
        JokeContext context = new(options);
        context.Set<Joke>().AddRange(JokeTestDatabase.TestJokesList);
        context.SaveChangesAsync();
        
        _client = factory.WithWebHostBuilder(builder =>
         {
             builder.ConfigureTestServices(services =>
             {
                 services.AddScoped(_ => options);
            });
         }).CreateClient();
    }

    [Fact]
    public async Task DefaultRoute_ReturnsExpectedString()
    {
        const string url = "";
        const string expected = " 😈 It's not a bug. It's an undocumented feature! 😈";
        
        var response = await _client.GetAsync(url);
        var stringResult = await response.Content.ReadAsStringAsync();

        Assert.Equal(expected, stringResult);
    }
    
    [Fact]
    public async Task Get_EndpointReturnSuccessAndCorrectContentType()
    {
        const string url = "/joke";
        
        var response = await _client.GetAsync(url);
        
        response.EnsureSuccessStatusCode(); // Status Code 200-299
        Assert.Equal("application/json; charset=utf-8", 
            response.Content.Headers.ContentType!.ToString());
    }
    
    [Theory]
    [InlineData("1")]
    [InlineData("2")]
    public async Task GetById__WhenCorrectJokeIdSpecifiedInRoute_ReturnsOKStatusCode(
        string id)
    {
        var url = $"/joke/{id}";

        var response = await _client.GetAsync(url);
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    
    [Theory]
    [InlineData("3")]
    [InlineData("x")]
    [InlineData("0")]
    [InlineData("11")]
    [InlineData("1 1")]
    [InlineData("!")]
    [InlineData(" ? ")]
    [InlineData(" ?1/ ")]
    public async Task GetById_WhenInvalidJokeIdSpecifiedInRoute_ReturnsNotFoundStatusCode(
        string id)
    {
        var url = $"/joke/{id}";

        var response = await _client.GetAsync(url);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
    
    [Fact]
    public async Task Post_WhenNewJokeSpecified_ReturnsCreatedStatusCode()
    {
        var client = new WebApplicationFactory<Program>().CreateClient();

        const string url = "/joke";
        JokeDto newJoke = new()
        {
            Question = "What Pokémon do people see in auctions?",
            Punchline = "Bidoof!"
        };

        var newJokeAsJsonContent = JsonContent.Create(newJoke);

        var response = await client.PostAsync(url, newJokeAsJsonContent);
        
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    } 

    [Theory] //ModelState already checks for empty string
    [InlineData("","Opps! Empty Question!")]
    [InlineData("Opps! Empty Punchline","")]
    [InlineData("","")]
    public async Task Post_WhenNewJokeHasInvalidEmptyEntry_ReturnsBadRequestStatusCode(string question, string punchline)
    {
        const string url = "/joke";
        JokeDto newJoke = new()
        {
            Question = question,
            Punchline = punchline
        };
    
        var newJokeAsJsonContent = JsonContent.Create(newJoke);
    
        var response = await _client.PostAsync(url, newJokeAsJsonContent);
    
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    } 
    
    [Fact]
    public async Task Put_WhenRequestMadeToUpdateJoke_ReturnsOKStatusCode()
    {
        const int id = 1;
        var url = $"/joke/{id}";
        JokeDto updateJoke = new()
        {
            Question = "What’s Pikachu’s favorite song?",
            Punchline = "The Hokey Pokemon",
        };
        var updateJokeAsJsonContent = JsonContent.Create(updateJoke);

        var response = await _client.PutAsync(url, updateJokeAsJsonContent);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    
    [Theory]
    [InlineData("x")]
    [InlineData("0")] 
    [InlineData("11")]
    [InlineData("1 1")]
    [InlineData("!")]
    [InlineData(" ? ")]
    [InlineData(" ?1/ ")]
    public async Task Put_WhenInvalidJokeIdSpecifiedInRoute_ReturnsBadRequestCode(string id)
    {
        var url = $"/joke/{id}";
        JokeDto updateJoke = new()
        {
            Question = "What’s Pikachu’s favorite song?",
            Punchline = "The Hokey Pokemon",
        };
        var updateJokeAsJsonContent = JsonContent.Create(updateJoke);

        var responseMsg = await _client.PutAsync(url, updateJokeAsJsonContent);

        Assert.Equal(HttpStatusCode.NotFound, responseMsg.StatusCode);
    }
    
    [Theory] 
    [InlineData("","Opps! Empty Question!")]
    [InlineData("Opps! Empty Punchline","")]
    [InlineData("","")]
    public async Task Put_WhenNewJokeHasInvalidEmptyEntry_ReturnsBadRequestStatusCode(string question, string punchline)
    {
        const string url = "/joke/2";
        JokeDto newJoke = new()
        {
            Question = question,
            Punchline = punchline
        };
    
        var newJokeAsJsonContent = JsonContent.Create(newJoke);
    
        var response = await _client.PutAsync(url, newJokeAsJsonContent);
    
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    } 
    
    [Theory]
    [InlineData("x")]
    [InlineData("0")] 
    [InlineData("11")]
    [InlineData("1 1")]
    [InlineData("!")]
    [InlineData(" ? ")]
    [InlineData(" ?1/ ")]
    public async Task Patch_WhenRequestMadeToUpdateJokePunchlineMadeWithInvalidId_ReturnsNotFoundResponse(string id)
    {
        var url = $"/joke/{id}";
        var patchDocument = new JsonPatchDocument<JokeDto>();
        patchDocument.Replace(j => j.Punchline, "This is driving me nuts!");
        var updatePatchDocumentAsJsonContent = JsonConvert.SerializeObject(patchDocument);
        var request = new StringContent(updatePatchDocumentAsJsonContent, Encoding.UTF8, "application/json-patch+json");

        var response = await _client.PatchAsync(url, request);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Delete_WhenRequestMadeWithValidId_ReturnsOKStatusCode()
    {
        const int id = 1;
        var url = $"/joke/{id}";
        
        var response = await _client.DeleteAsync(url);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    
    [Theory]
    [InlineData("x")]
    [InlineData("0")] 
    [InlineData("11")]
    [InlineData("1 1")]
    [InlineData("!")]
    [InlineData(" ? ")]
    [InlineData(" ?1/ ")]
    public async Task Delete_WhenRequestMadeWithInvalidId_ReturnsNotFoundStatusCode(string id)
    {
        var url = $"/joke/{id}";
        
        var response = await _client.DeleteAsync(url);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}

