using Microsoft.EntityFrameworkCore;

namespace JokeAPI.Model;

public class JokeContext : DbContext
{
    public JokeContext(DbContextOptions<JokeContext> options): base (options){}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>()
            .HasMany(c => c.Jokes)
            .WithOne(a => a.Category)
            .HasForeignKey(a => a.CategoryId);
        modelBuilder.Seed();
    }
    
    public DbSet<Joke>? Jokes { get; set; }
    
    public DbSet<Category>? Categories { get; set; }
}