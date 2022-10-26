using JokeAPI.Model;

namespace JokeAPI.Data;

public static class JokeDatabase
{
    public static readonly List<Joke> JokesList = new ()
    {
        new Joke()
        {
            Id = 1,
            Category = "Pikachu",
            Question = "Why can't you blindfold a Pokemon?",
            Answer = "Because it's going to Pikachu!",
        },
        new Joke()
        {
            Id = 2,
            Category = "General",
            Question = "What do you call a Pokemon who can't move very fast?",
            Answer = "A Slow-poke",
        },
        new Joke()
        {
            Id = 3,
            Category = "General",
            Question = "What is the Dracula's favourite Pokemon?",
            Answer = @"Koffin'",
        },
        new Joke()
        {
            Id = 4,
            Category = "Meowth",
            Question = "What do you call Meowth's reflection?",
            Answer = "A copycat.",
        },
        new Joke()
        {
            Id = 5,
            Category = "",
            Question = "",
            Answer = "",
        },
    };
}