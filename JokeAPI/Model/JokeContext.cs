using Microsoft.EntityFrameworkCore;

namespace JokeAPI.Model;

public class JokeContext : DbContext
{
    public JokeContext(DbContextOptions<JokeContext> options) : base(options) { }
    public DbSet<Joke> Jokes { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>()
            .HasMany(c => c.Jokes)
            .WithOne(a => a.Category)
            .HasForeignKey(a => a.CategoryId);
        modelBuilder.Seed();
    }
}