namespace BookStore.Application.DTOs.Book;

public class BookUpdateDto
{
    public string Titulo { get; set; } = string.Empty;
 
    public string Categoria { get; set; } = string.Empty;
    
    public DateTime DataPublicacao { get; set; }
    
    public decimal Preco { get; set; }
    
    public int QuantidadeEstoque { get; set; }
}