using JokeAPI.DTO;
using JokeAPI.Mapper;
using JokeAPI.Model;
using Microsoft.AspNetCore.JsonPatch;
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
        var joke = await _context.Jokes.FindAsync(id);
        return joke != null ? Ok(joke) : NotFound();
    }

    [HttpPost(Name = "PostOne")]
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

    [HttpPut("/joke/{id:int}", Name = "Put")]
    public async Task<ActionResult<Joke>> Put(int id, [FromBody] JokeDto jokeDto)
    {
        if (!ModelState.IsValid) return BadRequest();
        var jokeToUpdate = jokeDto.UpdateDomainModel(id);
        try
        {
            _context.Jokes.Update(jokeToUpdate);
            await _context.SaveChangesAsync();
            return Ok();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (await _context.Jokes.FindAsync(jokeToUpdate.Id) == null)
                return NotFound();
        }

        return NotFound();
    }

    [HttpPatch("/joke/{id:int}", Name = "Patch")]
    public async Task<ActionResult<Joke>> Patch([FromRoute] int id, [FromBody] JsonPatchDocument<JokeDto> patchDocument)
    {
        if (!ModelState.IsValid) return BadRequest();
        var joke = await _context.Jokes.FindAsync(id);
        if (joke == null) return NotFound();

        var jokeDto = new JokeDto();
        patchDocument.ApplyTo(jokeDto, ModelState);
        joke = jokeDto.UpdateDomainModel(id);
        
        _context.Entry(joke).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return Ok(joke);
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

