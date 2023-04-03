using Microsoft.EntityFrameworkCore;

namespace JokeAPI.Model;

/// <summary></summary>
public static class ModelBuilderExtensions
{
    /// <summary></summary>
    /// <param name="modelBuilder"></param>
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Joke>().HasData(
            new Joke()
            {
                Id = 1,
                Question = "Why can't you blindfold a Pokemon?",
                Punchline = "Because it's going to Pikachu!",
            },
            new Joke()
            {
                Id = 2,
                Question = "What do you call a Pokemon who can't move very fast?",
                Punchline = "A Slow-poke",
            },
            new Joke()
            {
                Id = 3,
                Question = "What is the Dracula's favourite Pokemon?",
                Punchline = @"Koffin'",
            },
            new Joke()
            {
                Id = 4,
                Question = "What do you call Meowth's reflection?",
                Punchline = "A copycat.",
            },
            new Joke()
            {
                Id = 5,
                Question = "Where do Pokemon go if their tails fall off?",
                Punchline = "A retail store",
            }
        );
    }
}

