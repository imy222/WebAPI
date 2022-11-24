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

    [HttpGet(Name = "GetJokes")]
    public async Task<ActionResult<List<Joke>>> GetAll()
    {
        return Ok(await _context.Jokes.ToListAsync());
    }
    
    [HttpGet("/joke/{id:int}", Name = "GetById")]
    public async Task<ActionResult<Joke>> GetById(int id)
    {
        var joke =  await _context.Jokes.FindAsync(id);
        return joke != null? Ok(joke) : NotFound();
    }

    [HttpPost(Name = "PostOne")]
    public async Task<ActionResult<Joke>> Post([FromBody] Joke joke)
    {
        if (!ModelState.IsValid) return BadRequest();
        _context.Jokes.Add(joke);
        await _context.SaveChangesAsync();
        return CreatedAtAction(
            "GetAll",
            new {id = joke.Id},
            joke
        );
    }

    [HttpPut("/joke/{id}", Name = "Put")]
    public async Task<ActionResult<Joke>> Put(int id, [FromBody] Joke joke)
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

    [HttpDelete("/joke/{id:int}")]
    public async Task<ActionResult<Joke>> Delete(int id)
    {
        var joke = await _context.Jokes.FindAsync(id);
        if (joke == null) return NotFound();
        _context.Jokes.Remove(joke);
        await _context.SaveChangesAsync();
        return Ok();
        
    }
}

