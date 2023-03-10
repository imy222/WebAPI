using Microsoft.EntityFrameworkCore;

namespace JokeAPI.Model;

/// <summary>
/// 
/// </summary>
public class JokeContext : DbContext
{
    /// <summary></summary>
    /// <param name="options"></param>
    public JokeContext(DbContextOptions<JokeContext> options) : base(options) { }
    
    /// <summary></summary>
    public DbSet<Joke> Jokes { get; set; } = null!;

    /// <summary></summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Seed();
    }
}