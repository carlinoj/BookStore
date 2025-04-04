using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Persistence;

/// <summary>
/// Mapeia as entidades no banco de dados.
/// </summary>
public class BookStoreDbContext : DbContext
{
    public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Author> Authors => Set<Author>();
    public DbSet<Book> Books => Set<Book>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<Book>()
            .HasOne(b => b.Autor)
            .WithMany(a => a.Livros)
            .HasForeignKey(b => b.AuthorId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Book>()
            .Property(b => b.Preco)
            .HasPrecision(18, 2); 

        modelBuilder.Entity<User>()
            .Property(u => u.Nome)
            .HasMaxLength(100);

        modelBuilder.Entity<Author>()
            .Property(a => a.Nome)
            .HasMaxLength(100);

        modelBuilder.Entity<Book>()
            .Property(b => b.Titulo)
            .HasMaxLength(150);
    }
}