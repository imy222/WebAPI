using JokeAPI.DTO;
using JokeAPI.Model;

namespace JokeAPI.Mapper;

public static class JokeMapper
{
    public static Joke ToDomain(this JokeDto jokeDto)
    {
        return new Joke()
        {
            Question = jokeDto.Question,
            Punchline = jokeDto.Punchline,
        };
    }
}