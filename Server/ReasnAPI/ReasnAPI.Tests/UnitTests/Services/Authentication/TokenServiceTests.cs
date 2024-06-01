using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;
using Moq;
using ReasnAPI.Models.Database;
using ReasnAPI.Models.Enums;
using ReasnAPI.Services.Authentication;

namespace ReasnAPI.Tests.UnitTests.Services.Authentication;

[TestClass]
public class TokenServiceTests
{
    private const int DurationInHours = 8;
    private const string IssAudValue = "http://localhost:5272";
    private TokenService _service = null!;
    private Mock<IConfiguration> _mockConfiguration = null!;
    private User _validUser = null!;
    
    [TestInitialize]
    public void Setup()
    {
        _mockConfiguration = new Mock<IConfiguration>();

        var bytes = new byte[32];
        RandomNumberGenerator.Fill(bytes);
        _mockConfiguration.Setup(x => 
            x["JwtSettings:Key"]).Returns(Convert.ToBase64String(bytes));
        
        _mockConfiguration.Setup(x => 
            x["JwtSettings:Issuer"]).Returns(IssAudValue);
        
        var mockSection = new Mock<IConfigurationSection>();
        var mockAudienceValue = new Mock<IConfigurationSection>();
        mockAudienceValue.Setup(x => x.Value).Returns(IssAudValue);
        mockSection.Setup(x =>
            x.GetChildren()).Returns(new List<IConfigurationSection> { mockAudienceValue.Object });
        _mockConfiguration.Setup(x =>
            x.GetSection("JwtSettings:Audiences")).Returns(mockSection.Object);
        
        var mockDurationValue = new Mock<IConfigurationSection>();
        mockDurationValue.SetupGet(x => x.Value).Returns(DurationInHours.ToString());
        _mockConfiguration.Setup(x =>
            x.GetSection("JwtSettings:DurationInHours")).Returns(mockDurationValue.Object);
        
        _service = new TokenService(_mockConfiguration.Object);
        
        _validUser = new User {
            Id = 1,
            Name = "Jon",
            Surname = "Snow",
            Email = "jon.snow@castleblack.com",
            Role = UserRole.User
        };
    }

    [TestMethod]
    public void GenerateToken_WhenValidUser_ShouldReturnTokenPayload()
    {
        var result = _service.GenerateToken(_validUser);
        
        Assert.IsNotNull(result);
        Assert.AreEqual("Bearer", result.TokenType);
        Assert.IsNotNull(result.AccessToken);
        Assert.AreEqual(DurationInHours * 60 * 60, result.ExpiresIn);
    }

    [TestMethod]
    public void GenerateToken_WhenValidUser_ShouldReturnValidToken()
    {
        var result = _service.GenerateToken(_validUser);
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.ReadToken(result.AccessToken) as JwtSecurityToken;
        
        Assert.IsNotNull(token);
        Assert.AreEqual(IssAudValue, token.Issuer);
        Assert.AreEqual(IssAudValue, token.Audiences.First());
        Assert.AreEqual(_validUser.Email, token.Subject);
        Assert.AreEqual(DurationInHours * 60 * 60, (token.ValidTo - token.ValidFrom).TotalSeconds);
    }
}