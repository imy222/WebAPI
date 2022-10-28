using Microsoft.EntityFrameworkCore;

namespace JokeAPI.Model;

public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder modelbuilder)
    {
        modelbuilder.Entity<Category>().HasData(
            new Category() { CategoryId = 1, Name = "Pikachu"},
            new Category() { CategoryId = 2, Name = "Meowth"},
            new Category() { CategoryId = 3, Name = "Other"}
        );

        modelbuilder.Entity<Joke>().HasData(
            new Joke()
            {
                Id = 1,
                Question = "Why can't you blindfold a Pokemon?",
                Punchline = "Because it's going to Pikachu!",
                CategoryId = 1,
            },
            new Joke()
            {
                Id = 2,
                Question = "What do you call a Pokemon who can't move very fast?",
                Punchline = "A Slow-poke",
                CategoryId = 2,
            },
            new Joke()
            {
                Id = 3,
                Question = "What is the Dracula's favourite Pokemon?",
                Punchline = @"Koffin'",
                CategoryId = 2,
            },
            new Joke()
            {
                Id = 4,
                Question = "What do you call Meowth's reflection?",
                Punchline = "A copycat.",
                CategoryId = 2,
            },
            new Joke()
            {
                Id = 5,
                Question = "Oh No! None found!",
                Punchline = "Hahahaha",
                CategoryId = 3,
            }
        );
    }
}

