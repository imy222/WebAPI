using System.ComponentModel.DataAnnotations;

namespace JokeAPI.DTO;

public class JokeDto
{
    [Required]
    public string? Question { get; set; }
    
    [Required]
    public string? Punchline { get; set; }
}