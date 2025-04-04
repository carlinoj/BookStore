namespace BookStore.Domain.Entities;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Nome { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;
    
    public string SenhaHash { get; set; } = string.Empty;
    
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    
    public string Role { get; set; } = "User"; // Admin ou User
}
