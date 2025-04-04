namespace BookStore.Application.Settings;

public class JwtSettings
{
    public string Secret { get; set; } = string.Empty;
 
    public string Issuer { get; set; } = "BookStoreAPI";
    
    public string Audience { get; set; } = "BookStoreClient";
    
    public int ExpirationMinutes { get; set; } = 60;
}