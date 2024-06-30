using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<Person> People { get; set; } // DbSet of Person entities

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("TestDb"); // Use in-memory database for testing
    }
}
