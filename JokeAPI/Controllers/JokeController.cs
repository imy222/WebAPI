using JokeAPI.Data;
using JokeAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace JokeAPI.Controllers;

[ApiController]
[Route("[controller]")]

public class JokeController : ControllerBase 
{
    //TODO Create local logger at later stage
    
    /*private readonly ILogger<JokeController> _logger;

    public JokeController(ILogger<JokeController> logger)
    {
        _logger = logger;
    }*/

    [HttpGet(Name = "GetAllJokes")]
    public IEnumerable<Joke> GetAllJokes()
    {
        return JokeDatabase.JokesList.ToArray();
    }
    
    //IAction Result and Ok, returns status code of API Call.
    [HttpGet("random",Name = "GetOneRandomJoke")]
    public IActionResult GetOneRandomJoke()
    {
        var random = new Random();
        var randomJokeId = random.Next(1,JokeDatabase.JokesList.Count()+1);
        var randomJoke = JokeDatabase.JokesList
            .Find(joke => joke.Id == randomJokeId);
        return Ok(randomJoke);
    }
    
    [HttpGet("/joke/{id:int}", Name = "GetJokeById")]
    public Joke GetSelectedJoke(int id)
    {
        var selectedJoke = JokeDatabase.JokesList
            .Find(joke => joke.Id == id);
        if (selectedJoke == null)
            return new Joke(){Id = 0, Category="Problem!", Question = "Oh No! None found!", Answer = "Hahahaha"};
        return selectedJoke;
    }
    
    //IAction Result and Ok, returns status code of API Call.
    /*[HttpGet("/joke/{id:int}", Name = "GetJokeById")]
    public IActionResult GetSelectedJoke(int id)
    {
        var selectedJoke = JokeDatabase.JokesList
            .Find(joke => joke.Id == id);
        if (selectedJoke == null)
            return NotFound();
        return Ok(selectedJoke);
    }*/
    
    
}
