using JokeAPI.Controllers;
using JokeAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JokeAPITests;

public class JokeControllerTests
{
    private readonly JokeController _jokeController;
    
    public JokeControllerTests()
    {
        var options = new DbContextOptionsBuilder<JokeContext>().UseInMemoryDatabase("database").Options;
        JokeContext context = new(options);
        context.Set<Joke>().AddRange(JokeTestDatabase.TestJokesList);
        context.SaveChangesAsync();
        _jokeController = new JokeController(context);
    }
    
    [Fact]
    public void GetAll_WhenRequestReceived_ReturnsDefaultStatusCode200()
    {
        var result = _jokeController.GetAll();

        Assert.IsType<OkObjectResult>(result.Result);
    }
    
    [Fact]
    public void GetAll_WhenRequestReceived_ReturnsListOfJokes()
    {
        var response = _jokeController.GetAll();

        //Assert result is a List<Joke>. Assert return TYPE
        var actual = response.Result as OkObjectResult;
        Assert.IsType<List<Joke>>(actual.Value);
    }
    
    [Fact]
    public void GetAll_WhenRequestReceived_ReturnsTotalNumberOfJokesInTestDatabase()
    {
        var expected = JokeTestDatabase.TestJokesList.Count;
        
        var response = _jokeController.GetAll();
        
        var actual= response.Result as OkObjectResult;
        var actualList = actual.Value as List<Joke>;
        Assert.Equal(expected, actualList.Count);
    }
    
    [Fact]
    public async Task GetById_WhenRequestReceivedWithValidIdNumberOne_ReturnsFirstJoke()
    {
        const string expected = "A copycat!";
        const int validTestId = 1;

        var response = await _jokeController.GetById(validTestId);
        
        Assert.IsType<OkObjectResult>(response.Result);
        
        var actual= response.Result as OkObjectResult;
        var actualList = actual.Value as Joke;
        Assert.Equal(expected, actualList.Punchline);
    }
    
    [Fact]
    public async Task GetById_WhenRequestReceivedWithInvalidIdNumberOne_ReturnsNotFoundAndNull()
    {
        const int invalidTestId = 3;

        var response = await _jokeController.GetById(invalidTestId);
        
        Assert.IsNotType<OkObjectResult>(response.Result);
        Assert.Null(response.Value);
    }
    
    [Fact]
    public async Task Post_WhenRequestReceived_AddNewJokeToListOfJokes()
    {
        Joke newJoke = new()
        {
            Id = 3,
            Question = "How do Pokemon watch cartoons?",
            Punchline = "On their Teevee",
            CategoryId = 3,
        };
        const string expected = "On their Teevee";

        var response = await _jokeController.Post(newJoke);

        Assert.IsType<CreatedAtActionResult>(response.Result);
        var actual= response.Result as CreatedAtActionResult;
        Assert.IsType<Joke>(actual.Value);
        var jokeItem = actual.Value as Joke;
        Assert.Equal(expected, jokeItem.Punchline);
    }
}


