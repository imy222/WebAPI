
namespace JokeAPI.Model;

public class Joke
{
    public int Id { get; set; }
    
    public string? Question { get; set; }

    public string? Punchline { get; set; }
}