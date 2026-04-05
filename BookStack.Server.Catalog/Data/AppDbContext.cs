using BookStack.Shared;
using Microsoft.EntityFrameworkCore;

namespace BookStack.Server.Catalog.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<BookDto> Books => Set<BookDto>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BookDto>(e =>
        {
            e.ToTable("books");
            e.HasKey(b => b.Id);
            e.Property(b => b.Title).IsRequired().HasMaxLength(300);
            e.Property(b => b.Author).IsRequired().HasMaxLength(200);
            e.Property(b => b.ISBN).HasMaxLength(20);
            e.Property(b => b.Genre).HasMaxLength(100);

            e.HasData(
                new BookDto { Id = 1, Title = "Der Prozess",  Author = "Franz Kafka",      ISBN = "978-3-16-148410-0", Genre = "Roman", Stock = 5 },
                new BookDto { Id = 2, Title = "Faust",        Author = "Johann W. Goethe", ISBN = "978-3-16-148410-1", Genre = "Drama", Stock = 3 },
                new BookDto { Id = 3, Title = "Effi Briest",  Author = "Theodor Fontane",  ISBN = "978-3-16-148410-2", Genre = "Roman", Stock = 2 }
            );
        });
    }
}
