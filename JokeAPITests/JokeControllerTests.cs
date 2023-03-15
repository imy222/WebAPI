using JokeAPI.Controllers;
using JokeAPI.DTO;
using JokeAPI.Model;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JokeAPITests;

public class JokeControllerTests
{
    private readonly DbContextOptions<JokeContext> _options;

    public JokeControllerTests()
    {
        _options = new DbContextOptionsBuilder<JokeContext>().UseInMemoryDatabase("database").Options;
        var context = new JokeContext(_options);
        context.Set<Joke>().AddRange(JokeTestDatabase.TestJokesList);
        context.SaveChangesAsync();
        context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    [Fact]
    public async Task GetJokes_WhenRequestReceived_ReturnsDefaultStatusCode200()
    {
        await using JokeContext newContext = new(_options);
        JokeController jokeController = new(newContext);

        var result = await jokeController.GetJokes();

        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task GetJokes_WhenRequestReceived_ReturnsListOfJokes()
    {
        await using JokeContext newContext = new(_options);
        JokeController jokeController = new(newContext);

        var response = await jokeController.GetJokes();

        var actual = response.Result as OkObjectResult;
        Assert.IsType<List<Joke>>(actual!.Value);
    }

    [Fact]
    public async Task GetJokes_WhenRequestReceived_ReturnsTotalNumberOfJokesInTestDatabase()
    {
        await using JokeContext newContext = new(_options);
        JokeController jokeController = new(newContext);
        var expected = JokeTestDatabase.TestJokesList.Count;

        var response = await jokeController.GetJokes();

        var actual = response.Result as OkObjectResult;
        var actualList = actual!.Value as List<Joke>;
        Assert.Equal(expected, actualList!.Count);
    }

    [Fact]
    public async Task GetById_WhenRequestReceivedWithValidIdNumberOne_ReturnsFirstJokePunchline()
    {
        await using JokeContext newContext = new(_options);
        JokeController jokeController = new(newContext);
        const string expected = "A copycat!";
        const int validTestId = 1;

        var response = await jokeController.GetById(validTestId);

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
        await using JokeContext newContext = new(_options);
        JokeController jokeController = new(newContext);

        var response = await jokeController.GetById(invalidId);

        Assert.IsType<NotFoundResult>(response.Result);
        Assert.Null(response.Value);
    }

    [Fact]
    public async Task Post_WhenRequestReceived_AddNewJokeToListOfJokes()
    {
        await using JokeContext newContext = new(_options);
        JokeController jokeController = new(newContext);
        JokeDto newJoke = new()
        {
            Question = "How do Pokemon watch cartoons?",
            Punchline = "On their Teevee",
        };
        const string expected = "On their Teevee";

        var response = await jokeController.Post(newJoke);

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
        JokeController jokeController = new(newContext);
        jokeController.ModelState.AddModelError("Question", "Question is a required field");
        JokeDto newJoke = new();

        var response = await jokeController.Post(newJoke);

        Assert.IsType<BadRequestResult>(response.Result);
    }

    [Fact]
    public async Task Put_WhenRequestReceivedWithValidId_UpdatesSelectedJoke()
    {
        await using JokeContext newContext = new(_options);
        JokeController jokeController = new(newContext);
        const int validId = 1;
        JokeDto jokeDto = new()
        {
            Question = "What do you call Meowth's reflection?",
            Punchline = "A work of art!",
        };
        const string expected = "A work of art!";

        var response = await jokeController.Put(validId, jokeDto);
        var jokeItem = await jokeController.GetById(validId);

        Assert.IsNotType<BadRequestResult>(response.Result);
        var actual = jokeItem.Result as OkObjectResult;
        var updatedJoke = actual!.Value as Joke;
        Assert.Equal(expected, updatedJoke!.Punchline);
    }


    [Theory]
    [InlineData(-1)]
    [InlineData(6)]
    [InlineData(101)]
    public async Task Put_WhenRequestReceivedWithIdThatDoesNotMatchJokeId_ReturnsNotFoundResponse(int invalidId)
    {
        await using JokeContext newContext = new(_options);
        JokeController testController = new(newContext);
        JokeDto jokeDto = new()
        {
            Question = "What do you call Meowth's reflection?",
            Punchline = "A work of art!",
        };

        var response = await testController.Put(invalidId, jokeDto);

        Assert.IsType<NotFoundResult>(response.Result);
    }

    [Fact]
    public async Task Patch_WhenRequestReceivedWithValidId_UpdatesJokePunchline()
    {
        await using JokeContext newContext = new(_options);
        JokeController jokeController = new(newContext);
        const int validId = 2;

        var patchDocument = new JsonPatchDocument<JokeDto>();
        patchDocument.Replace(j => j.Punchline, "This is driving me nuts!");
        const string originalQuestion = "What does a Pokemon say when it gets a cold?";
        const string expectedUpdatedPunchline = "This is driving me nuts!";

        var patchResponse = await jokeController.Patch(validId, patchDocument);
        var getResponse = await jokeController.GetById(validId);

        Assert.IsType<OkObjectResult>(patchResponse.Result);
        var actual = getResponse.Result as OkObjectResult;
        var actualList = actual!.Value as Joke;
        Assert.Equal(originalQuestion, actualList!.Question);
        Assert.Equal(expectedUpdatedPunchline, actualList.Punchline);
    }

    [Fact]
    public async Task Patch_WhenRequestReceivedWithInValidId_ReturnsNotFoundResponse()
    {
        await using JokeContext newContext = new(_options);
        JokeController jokeController = new(newContext);
        const int invalidId = 5;
       
        var patchDocument = new JsonPatchDocument<JokeDto>();
        patchDocument.Add(j => j.Punchline, "This is driving me nuts!");
        
        var response = await jokeController.Patch(invalidId, patchDocument);

        Assert.IsType<NotFoundResult>(response.Result);
    }

    [Fact]
    public async Task Delete_WhenRequestReceivedWithValidId_DeletesJokeFromDatabase()
    {
        await using JokeContext newContext = new(_options);
        JokeController jokeController = new(newContext);
        const int validId = 1;
        const int expectedNumberOfJokesRemaining = 2;

        var okResult = await jokeController.Delete(validId);

        Assert.IsType<OkResult>(okResult.Result);
        var response = await jokeController.GetJokes();
        var actual = response.Result as OkObjectResult;
        var actualList = actual!.Value as List<Joke>;
        Assert.Equal(expectedNumberOfJokesRemaining , actualList!.Count);
    }

    [Fact]
    public async Task Delete_WhenRequestReceivedWithIdThatDoesNotExist_ReturnsNotFoundResponseAndNumberOfJokesHaveNotChanged()
    {
        await using JokeContext newContext = new(_options);
        JokeController jokeController = new(newContext);
        const int invalidId = 5;
        const int expectedNumberOfJokesRemaining = 3;

        var okResult = await jokeController.Delete(invalidId);

        Assert.IsType<NotFoundResult>(okResult.Result);
        var response = await jokeController.GetJokes();
        var actual = response.Result as OkObjectResult;
        var actualList = actual!.Value as List<Joke>;
        Assert.Equal(expectedNumberOfJokesRemaining, actualList!.Count);
    }
}
