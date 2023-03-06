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
    public async Task Get_EndpointReturnSuccessAndCorrectContentType()
    {
        const string url = "/joke";
        
        var response = await _client.GetAsync(url);
        
        response.EnsureSuccessStatusCode(); // Status Code 200-299
        Assert.Equal("application/json; charset=utf-8", 
            response.Content.Headers.ContentType!.ToString());
    }
}