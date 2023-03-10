using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JokeAPI.Model;

/// <summary>
/// Joke
/// </summary>
public class Joke
{
    /// <summary>
    /// Joke Id
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; init; }
    
    /// <summary>
    /// Joke Question
    /// </summary>
    [Required]
    public string? Question { get; set; }
    
    /// <summary>
    /// Joke Punchline
    /// </summary>
    [Required]
    public string? Punchline { get; set; }
}