using JokeAPI.Model;

namespace JokeAPITests;

public static class JokeTestDatabase
{
    public static readonly List<Joke> TestJokesList = new()
    {
        new Joke()
        {
            Id = 1,
            Question = "What do you call Meowth's reflection?",
            Punchline = "A copycat!",
        },
        new Joke()
        {
            Id = 2,
            Question = "What does a Pokemon say when it gets a cold?",
            Punchline = "Pik-Achoo!",
        },
        new Joke()
        {
            Id = 3,
            Question = "Why can’t you blindfold a Pokémon?",
            Punchline = "Because it’s going to Pikachu!",
        },
    };
}