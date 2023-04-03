using JokeAPI.DTO;
using JokeAPI.Model;

namespace JokeAPI.Mapper;

/// <summary></summary>
public static class JokeMapper
{
    /// <summary></summary>
    /// <param name="jokeDto"></param>
    /// <returns></returns>
    public static Joke CreateDomainModel(this JokeDto jokeDto)
    {
        return new Joke()
        {
            Question = jokeDto.Question,
            Punchline = jokeDto.Punchline,
        };
    }
    
    /// <summary></summary>
    /// <param name="jokeDto"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public static Joke UpdateDomainModel(this JokeDto jokeDto, int id)
    {
        return new Joke()
        {
            Id = id,
            Question = jokeDto.Question,
            Punchline = jokeDto.Punchline,
        };
    }
    
    /// <summary></summary>
    /// <param name="joke"></param>
    /// <returns></returns>
    public static JokeDto CreateDto(this Joke joke)
    {
        return new JokeDto()
        {
            Question = joke.Question,
            Punchline = joke.Punchline,
        };
    }
}