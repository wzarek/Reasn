using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using ReasnAPI.Models.Authentication;
using ReasnAPI.Models.Database;

namespace ReasnAPI.Services.Authentication;

public class TokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration) =>
        _configuration = configuration;

    public TokenPayload GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenLifetime = TimeSpan.FromHours(
            _configuration.GetValue<int>("JwtSettings:DurationInHours"));

        var key = Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]!);
        var issuer = _configuration["JwtSettings:Issuer"]!;
        var audiences = _configuration.GetSection("JwtSettings:Audiences")
            .Get<IEnumerable<string>>()!;

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim(JwtRegisteredClaimNames.Iss, issuer),
                new Claim(JwtRegisteredClaimNames.Aud, string.Join(",", audiences))
            }),
            Expires = DateTime.UtcNow.Add(tokenLifetime),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenPayload = new TokenPayload
        {
            TokenType = "Bearer",
            AccessToken = tokenHandler.WriteToken(token),
            ExpiresIn = Convert.ToInt32(tokenLifetime.TotalSeconds)
        };
        return tokenPayload;
    }
}