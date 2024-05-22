namespace Infrastructure.JWT;


public class TokenSettings
{
    public string SecretKey { get; set; } = null!;
    public int ExpirationInMinutes { get; set; }
}
