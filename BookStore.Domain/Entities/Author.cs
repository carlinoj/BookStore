namespace BookStore.Domain.Entities;

/// <summary>
/// Representa um autor de livros.
/// </summary>
public class Author
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public string Nome { get; set; } = string.Empty;
    
    public DateTime DataNascimento { get; set; }
    
    public string Pais { get; set; } = string.Empty;

    // Navegação (opcional)
    public ICollection<Book>? Livros { get; set; }
}