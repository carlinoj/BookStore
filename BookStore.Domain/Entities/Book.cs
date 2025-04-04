namespace BookStore.Domain.Entities;

/// <summary>
/// Representa um livro na livraria.
/// </summary>
public class Book
{
    public Guid Id { get; set; } = Guid.NewGuid();
 
    public string Titulo { get; set; } = string.Empty;
    
    public Guid AuthorId { get; set; }
    
    public string Categoria { get; set; } = string.Empty;
    
    public DateTime DataPublicacao { get; set; }
    
    public decimal Preco { get; set; }
    
    public int QuantidadeEstoque { get; set; }

    // Navegação (opcional)
    public Author? Autor { get; set; }
}