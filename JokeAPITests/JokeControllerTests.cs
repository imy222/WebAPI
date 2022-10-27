using JokeAPI.Controllers;

namespace JokeAPITests;

public class JokeControllerTests
{
    private readonly JokeController _jokeController = new();
    
    [Fact]
    public void GetAllJokes_WhenCall_ReturnsAllFiveNumberOfJokesInJokesList()
    {
        const int expected = 5;

        var actual = _jokeController.GetAllJokes().Count();
        
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void GetSelectedJoke_WhenGivenAValidIDNumber_ReturnsKoffinAsJokeAnswer()
    {
        const string expected = "Koffin'";
        const int id = 3;

        var joke = _jokeController.GetSelectedJoke(id);
        var actual = joke.Answer;

        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void GetSelectedJoke_WhenGivenANonExistingIDNumber_ReturnOhNoNoneFound()
    {
        const string expected = "Oh No! None found!";
        const int id = 10;

        var joke = _jokeController.GetSelectedJoke(id);
        var actual = joke.Question;

        Assert.Equal(expected, actual);
    }
}
