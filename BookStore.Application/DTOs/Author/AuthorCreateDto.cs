namespace BookStore.Application.DTOs.Author;

public class AuthorCreateDto
{
    public string Nome { get; set; } = string.Empty;
 
    public DateTime DataNascimento { get; set; }
    
    public string Pais { get; set; } = string.Empty;
}