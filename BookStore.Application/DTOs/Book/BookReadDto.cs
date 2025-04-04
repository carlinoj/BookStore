namespace BookStore.Application.DTOs.Book;

public class BookReadDto
{
    public Guid Id { get; set; }
   
    public string Titulo { get; set; } = string.Empty;
    
    public string Categoria { get; set; } = string.Empty;
    
    public DateTime DataPublicacao { get; set; }
    
    public decimal Preco { get; set; }
    
    public int QuantidadeEstoque { get; set; }
    
    public Guid AuthorId { get; set; }
    
    public string AuthorNome { get; set; } = string.Empty;
}
