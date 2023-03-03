using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JokeAPI.Model;

public class Joke
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    public string? Question { get; set; }
    
    [Required]
    public string? Punchline { get; set; }
}