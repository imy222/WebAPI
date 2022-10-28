using JokeAPI.Data;
using JokeAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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

    private readonly JokeContext _context;

    public JokeController (JokeContext context)
    {
        _context = context;
        _context.Database.EnsureCreated();
    }
    
    [HttpGet(Name = "GetAllJokes")]
    public IEnumerable<Joke> GetAllJokes()
    {
        return _context.Jokes.ToArray();
    }
    
    //IAction Result and Ok, returns status code of API Call.
    [HttpGet("/random",Name = "GetOneRandomJoke")]
    public IActionResult GetOneRandomJoke()
    {
        var random = new Random();
        var randomJokeId = random.Next(1,_context.Jokes.Count()+1);
        var randomJoke = _context.Jokes
            .Find(randomJokeId);
        return Ok(randomJoke);
    }
    
    [HttpGet("/joke/{id:int}", Name = "GetJokeById")]
    public Joke GetSelectedJoke(int id)
    {
        var selectedJoke = _context.Jokes
            .Find(id);
        return selectedJoke ?? _context.Jokes.Last();
    }
    
    //IAction Result and Ok, returns status code of API Call.
    /*[HttpGet("/joke/{id:int}", Name = "GetJokeById")]
    public IActionResult GetSelectedJoke(int id)
    {
        var selectedJoke = JokeDatabase.JokesList
            .Find(joke => joke.CategoryId == id);
        if (selectedJoke == null)
            return NotFound();
        return Ok(selectedJoke);
    }*/
    
    
}
