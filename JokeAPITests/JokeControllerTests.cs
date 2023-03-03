using JokeAPI.Controllers;
using JokeAPI.DTO;
using JokeAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JokeAPITests;

public class JokeControllerTests
{
    private readonly JokeController _jokeController;
    private readonly DbContextOptions<JokeContext> _options;

    public JokeControllerTests()
    {
        _options = new DbContextOptionsBuilder<JokeContext>().UseInMemoryDatabase("database").Options;
        JokeContext context = new(_options);
        context.Set<Joke>().AddRange(JokeTestDatabase.TestJokesList);
        context.SaveChangesAsync();
        _jokeController = new JokeController(context);
    }

    [Fact]
    public async Task GetAll_WhenRequestReceived_ReturnsDefaultStatusCode200()
    {
        var result = await _jokeController.GetAll();

        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task GetAll_WhenRequestReceived_ReturnsListOfJokes()
    {
        var response = await _jokeController.GetAll();

        var actual = response.Result as OkObjectResult;
        Assert.IsType<List<Joke>>(actual!.Value);
    }

    [Fact]
    public async Task GetAll_WhenRequestReceived_ReturnsTotalNumberOfJokesInTestDatabase()
    {
        var expected = JokeTestDatabase.TestJokesList.Count;

        var response = await _jokeController.GetAll();

        var actual = response.Result as OkObjectResult;
        var actualList = actual!.Value as List<Joke>;
        Assert.Equal(expected, actualList!.Count);
    }

    [Fact]
    public async Task GetById_WhenRequestReceivedWithValidIdNumberOne_ReturnsFirstJokePunchline()
    {
        const string expected = "A copycat!";
        const int validTestId = 1;

        var response = await _jokeController.GetById(validTestId);

        var actual = response.Result as OkObjectResult;
        var actualList = actual!.Value as Joke;
        Assert.Equal(expected, actualList!.Punchline);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(6)]
    [InlineData(101)]
    public async Task GetById_WhenRequestReceivedWithOutOfRangeId_ReturnsNotFoundAndNull(int invalidId)
    {
        var response = await _jokeController.GetById(invalidId);

        Assert.IsType<NotFoundResult>(response.Result);
        Assert.Null(response.Value);
    }
    //TODO add integration test where user enter invalid id data type

    [Fact]
    public async Task Post_WhenRequestReceived_AddNewJokeToListOfJokes()
    {
        //have to start a new instance of testController
        await using JokeContext newContext = new(_options);
        JokeController testController = new(newContext);
        JokeDto newJoke = new()
        {
            Question = "How do Pokemon watch cartoons?",
            Punchline = "On their Teevee",
        };
        const string expected = "On their Teevee";

        var response = await testController.Post(newJoke);

        Assert.IsType<CreatedAtActionResult>(response.Result);
        var actual = response.Result as CreatedAtActionResult;
        Assert.IsType<Joke>(actual!.Value);
        var jokeItem = actual.Value as Joke;
        Assert.Equal(expected, jokeItem!.Punchline);
    }

    [Fact]
    public async Task Post_WhenRequestReceivedAndModelStateIsInError_ReturnsBadRequest()
    {
        await using JokeContext newContext = new(_options);
        JokeController testController = new(newContext);
        testController.ModelState.AddModelError("Question", "Question is a required field");
        JokeDto newJoke = new();

        var response = await testController.Post(newJoke);

        Assert.IsType<BadRequestResult>(response.Result);
    }

    //TODO extend diff type of invalid IDs (out of range and incorrect type)

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(6)]
    [InlineData(101)]
    public async Task Put_WhenRequestReceivedWithIdThatDoesNotMatchJokeId_ReturnsBadRequestResponse(int invalidId)
    {
        await using JokeContext newContext = new(_options);
        JokeController testController = new(newContext);
        Joke joke = new()
        {
            Id = 1,
            Question = "What do you call Meowth's reflection?",
            Punchline = "A work of art!",
        };

        var response = await testController.Put(invalidId, joke);

        Assert.IsType<BadRequestResult>(response.Result);
    }

    [Fact]
    public async Task Put_WhenRequestReceivedWithValidId_UpdatesSelectedJoke()
    {
        await using JokeContext newContext = new(_options);
        JokeController testController = new(newContext);
        const int validId = 1;
        Joke joke = new()
        {
            Id = 1,
            Question = "What do you call Meowth's reflection?",
            Punchline = "A work of art!",
        };
        const string expected = "A work of art!";

        var response = await testController.Put(validId, joke);
        var jokeItem = await testController.GetById(validId);

        Assert.IsNotType<BadRequestResult>(response.Result);
        var actual = jokeItem.Result as OkObjectResult;
        var updatedJoke = actual!.Value as Joke;
        Assert.Equal(expected, updatedJoke!.Punchline);
    }

    [Fact]
    public async Task Delete_WhenRequestReceivedWithValidId_DeletesJokeFromDatabase()
    {
        await using JokeContext newContext = new(_options);
        JokeController testController = new(newContext);
        const int validId = 1;

        var okResult = await testController.Delete(validId);

        Assert.IsType<OkResult>(okResult.Result);
        var response = await _jokeController.GetAll();
        var actual = response.Result as OkObjectResult;
        var actualList = actual!.Value as List<Joke>;
        Assert.Single(actualList!);
    }

    [Fact]
    public async Task Delete_WhenRequestReceivedWithIdThatDoesNotExist_ReturnsNotFound()
    {
        await using JokeContext newContext = new(_options);
        JokeController testController = new(newContext);
        const int invalidId = 5;
        const int expectedNumberOfJokes = 2;

        var okResult = await testController.Delete(invalidId);

        Assert.IsNotType<OkResult>(okResult.Result);
        var response = await _jokeController.GetAll();
        var actual = response.Result as OkObjectResult;
        var actualList = actual!.Value as List<Joke>;
        Assert.Equal(expectedNumberOfJokes, actualList!.Count);
    }
}
