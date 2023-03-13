using JokeAPI.DTO;
using JokeAPI.Mapper;
using JokeAPI.Model;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JokeAPI.Controllers;

/// <summary>Joke Controller</summary>

[ApiController]
[Route("/joke")]

public class JokeController : ControllerBase
{
    private readonly JokeContext _context;

    /// <summary>
    /// Constructor for JokeController
    /// </summary>
    /// <param name="context"></param>
    public JokeController(JokeContext context)
    { 
        _context = context;
        _context.Database.EnsureCreated();
    }

    /// <summary>
    ///  Get all jokes
    /// </summary>
    [HttpGet(Name = "GetJokes")]
    public async Task<ActionResult<List<Joke>>> GetJokes()
    {
        return Ok(await _context.Jokes.ToListAsync());
    }

    /// <summary>
    ///  Get a joke by Id
    /// </summary>
    /// <param name="id"></param>
    [HttpGet("/joke/{id:int}", Name = "GetById")]
    public async Task<ActionResult<Joke>> GetById(int id)
    {
        var joke = await _context.Jokes.FindAsync(id);
        return joke != null ? Ok(joke) : NotFound();
    }

    /// <summary>
    ///  Create one new joke
    /// </summary>
    /// <param name="jokeDto"></param>
    [HttpPost(Name = "Post")]
    public async Task<ActionResult<Joke>> Post([FromBody] JokeDto jokeDto)
    {
        if (!ModelState.IsValid) return BadRequest();

        var joke = jokeDto.CreateDomainModel();
        _context.Jokes.Add(joke);
        await _context.SaveChangesAsync();
        return CreatedAtAction(
            "GetByID",
            new {id = joke.Id},
            joke
        );
    }

    /// <summary>
    ///  Update one existing joke selected by Id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="jokeDto"></param>
    [HttpPut("/joke/{id:int}", Name = "Put")]
    public async Task<ActionResult<Joke>> Put(int id, [FromBody] JokeDto jokeDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var jokeToUpdate = jokeDto.UpdateDomainModel(id);
        _context.Entry(jokeToUpdate).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
            return Ok();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            if (ex.Message.Contains("does not exist in the store"))
            {
                return NotFound();
            }
            throw;
        }
    }

    /// <summary>
    ///  Update either the Question or Punchline of an existing joke selected by Id
    /// </summary>
    /// <param name="id">Joke id</param>
    /// <param name="patchDocument"></param>
    [HttpPatch("/joke/{id:int}", Name = "Patch")]
    public async Task<ActionResult<Joke>> Patch([FromRoute] int id, [FromBody] JsonPatchDocument<JokeDto> patchDocument)
    {
        var joke = await _context.Jokes.FindAsync(id);
        if (joke == null) return NotFound();
        
        var jokeDto = joke.CreateDto();
        patchDocument.ApplyTo(jokeDto, ModelState);
        
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var updatedJoke = jokeDto.UpdateDomainModel(id);
        _context.Entry(joke).CurrentValues.SetValues(updatedJoke);
        await _context.SaveChangesAsync();

        return Ok(updatedJoke);
    }
    
    /// <summary>
    ///  Delete one joke selected by Id
    /// </summary>
    /// <param name="id"></param>
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

