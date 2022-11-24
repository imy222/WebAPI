using System.Text.Json.Serialization;

namespace JokeAPI.Model;

public class Joke
{
    public int Id { get; set; }
    
    public string Question { get; set; }

    public string Punchline { get; set; }
    
    public int CategoryId { get; set; }
    
    [JsonIgnore]
    public virtual Category? Category { get; set; }
}