using JokeAPI.DTO;
using JokeAPI.Model;

namespace JokeAPI.Mapper;

public static class JokeMapper
{
    public static Joke CreateDomainModel(this JokeDto jokeDto)
    {
        return new Joke()
        {
            Question = jokeDto.Question,
            Punchline = jokeDto.Punchline,
        };
    }
    
    public static Joke UpdateDomainModel(this JokeDto jokeDto, int id)
    {
        return new Joke()
        {
            Id = id,
            Question = jokeDto.Question,
            Punchline = jokeDto.Punchline,
        };
    }
}