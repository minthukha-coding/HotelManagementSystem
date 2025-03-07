using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HotelManagementSystem.Shared.Services;

public class JwtTokenService
{
    private readonly string _key;
    private readonly string _issuer;
    private readonly string _audience;
    private readonly int _expiryInMinutes;

    private readonly IConfiguration _configuration;
    private readonly JwtSecurityTokenHandler _tokenHandler;

    public JwtTokenService(
        IConfiguration configuration,
        JwtSecurityTokenHandler tokenHandler)
    {
        _configuration = configuration;
        _tokenHandler = tokenHandler;
        _key = configuration["Jwt:Key"]!;
        _issuer = configuration["Jwt:Issuer"]!;
        _audience = configuration["Jwt:Audience"]!;
        _expiryInMinutes = int.Parse(configuration["Jwt:ExpiryInMinutes"]!);
    }

    public string GenerateJwtToken(AccessTokenRequestModel requestModel)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
                new Claim(JwtRegisteredClaimNames.Sub, requestModel.UserId),
                new Claim(JwtRegisteredClaimNames.Email, requestModel.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_expiryInMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public ClaimsPrincipal ReadJwtToken(string token)
    {
        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!);
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };

        try
        {
            var principal = _tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);
            return principal;
        }
        catch
        {
            return null;
        }
    }

    public class AccessTokenRequestModel
    {
        public string UserId { get; set; }
        public string Email { get; set; }
    }
}