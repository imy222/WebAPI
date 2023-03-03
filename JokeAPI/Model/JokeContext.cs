using Microsoft.EntityFrameworkCore;

namespace JokeAPI.Model;

public class JokeContext : DbContext
{
    public JokeContext(DbContextOptions<JokeContext> options) : base(options) { }
    public DbSet<Joke> Jokes { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Seed();
    }
}