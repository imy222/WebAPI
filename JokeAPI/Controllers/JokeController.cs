using JokeAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JokeAPI.Controllers;

[ApiController]
[Route("/joke")]

public class JokeController : ControllerBase
{
    //TODO Create local logger at later stage

    /*private readonly ILogger<JokeController> _logger;

    public JokeController(ILogger<JokeController> logger)
    {
        _logger = logger;
    }*/

    private readonly JokeContext _context;

    public JokeController(JokeContext context)
    {
        _context = context;
        _context.Database.EnsureCreated();
    }

    [HttpGet("/test", Name = "GetAllJokes")]
    public IEnumerable<Joke> GetAllJokes()
    {
        return _context.Jokes.ToArray();
    }

    //IAction Result and Ok, returns status code of API Call.
    [HttpGet("/test/random", Name = "GetOneRandomJoke")]
    public IActionResult GetOneRandomJoke()
    {
        var random = new Random();
        var randomJokeId = random.Next(1, _context.Jokes.Count() + 1);
        var randomJoke = _context.Jokes
            .Find(randomJokeId);
        return Ok(randomJoke);
    }

    [HttpGet("/test/{id:int}", Name = "GetJokeById")]
    public Joke GetSelectedJoke(int id)
    {
        var selectedJoke = _context.Jokes
            .Find(id);
        return selectedJoke ?? _context.Jokes.Last();
    }

    /// Request without unit test

    [HttpGet(Name = "GetJokes")]
    public ActionResult<Joke> GetJokes()
    {
        return Ok(_context.Jokes.ToArray());
    }

    [HttpPost(Name = "PostOne")]
    public async Task<ActionResult<Joke>> PostJoke([FromBody] Joke joke)
    {
        _context.Jokes.Add(joke);
        await _context.SaveChangesAsync();
        return CreatedAtAction(
            "GetJokes",
            new {id = joke.Id},
            joke
        );
    }

    [HttpPost("{id}", Name = "Put")]
    public async Task<ActionResult<Joke>> PutJoke(int id, [FromBody] Joke joke)
    {
        if (id != joke.Id)
            return BadRequest();
        
        _context.Entry(joke).State = EntityState.Modified;
    
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (await _context.Jokes.FindAsync(id) == null)
            {
                return NotFound();
            }
            throw;
        }
        return NoContent();
    }
}
