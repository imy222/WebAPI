using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;

namespace JokeAPITests;

public class JokesIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public JokesIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions());
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
        string url = $"/joke/{id}";

        var response = await _client.GetAsync(url);

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
    public async Task GetById_WhenInvalidJokeIdSpecifiedInRoute_ReturnsNotFoundStatusCode(
        string id)
    {
        var url = $"/joke/{id}";

        var response = await _client.GetAsync(url);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
    
    [Theory]
    [InlineData("??")]// returns all
    [InlineData("?3")]// returns all
    [InlineData("3?")]// returns joke 3
    [InlineData(" 1 ")]// returns joke 1
    public async Task GetById_WhenFunnyJokeIdSpecifiedInRoute_ReturnsOkStatusCode(
        string id)
    {
        string url = $"/joke/{id}";

        var response = await _client.GetAsync(url);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}

