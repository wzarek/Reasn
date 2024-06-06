namespace ReasnAPI.Models.Authentication;

public class TokenPayload
{
    public string TokenType { get; set; } = null!;
    public string AccessToken { get; set; } = null!;
    public int ExpiresIn { get; set; }
}