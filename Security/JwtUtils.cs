using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using SiPerpusApi.Dto;
using SiPerpusApi.Models;

namespace SiPerpusApi.Security;

public class JwtUtils : IJwtUtils
{
    private readonly IConfiguration _configuration;

    public JwtUtils(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Tokens GenerateToken(User user)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]);

            DateTime exp = DateTime.Now.AddHours(int.Parse(_configuration["JwtSettings:ExpiresInMinutes"]));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = _configuration["JwtSettings:Audience"],
                Issuer = _configuration["JwtSettings:Issuer"],
                Expires = exp,
                IssuedAt = DateTime.Now,
                Subject = new ClaimsIdentity(new List<Claim>
                {
                    new(ClaimTypes.Email, user.Email),
                    new(ClaimTypes.Role, user.Role.RoleName)
                }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            
            Console.WriteLine("Role : " + user.Role.RoleNameEnum.ToString());

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var refreshToken = GenerateRefreshToken();

            Tokens sendTokens = new Tokens()
            {
                AccessToken = tokenHandler.WriteToken(token),
                RefreshToken = refreshToken,
                ExpiredAt = ((DateTimeOffset)exp).ToUnixTimeMilliseconds()
            };

            return sendTokens;
        }
        catch (Exception e)
        {
            Console.WriteLine("token error " + e);
            throw;
        }
    }
    
    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}