using System.ComponentModel.DataAnnotations;

namespace JokeAPI.DTO;

/// <summary>
/// JokeDto
/// </summary>
public class JokeDto
{
    /// <summary>
    /// Joke Question
    /// </summary>
    [Required]
    public string? Question { get; init; }
    
    /// <summary>
    /// Joke Punchline
    /// </summary>
    [Required]
    public string? Punchline { get; init; }
}