using Microsoft.AspNetCore.Mvc;

namespace JokeAPI.Model;

public class Joke
{
    public int Id { get; set; }
    
    public string Category { get; set; }

    public string Question { get; set; }

    public string Answer { get; set; }
}