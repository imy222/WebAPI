namespace JokeAPI.Model;

public class Category
{
    
    public int CategoryId { get; set; }

    public string Name { get; set; }

    public virtual List<Joke> Jokes { get; set; }
    
}