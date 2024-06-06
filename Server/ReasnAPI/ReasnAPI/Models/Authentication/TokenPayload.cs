namespace ReasnAPI.Models.Authentication;

public class TokenPayload
{
    public string TokenType { get; set; }
    public string AccessToken { get; set; }
    public int ExpiresIn { get; set; }
}