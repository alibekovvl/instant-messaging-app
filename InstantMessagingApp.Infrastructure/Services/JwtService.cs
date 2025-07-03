using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using InstantMessagingApp.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace InstantMessagingApp.Infrastructure.Services;

public class JwtService(IOptions<AuthSettings> options)
{
    public string GenerateToken(User user)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, user.Username), 
            new Claim("userName", user.Username),
            new Claim("id", user.Id.ToString())
        };
        var jwtToken = new JwtSecurityToken(
            expires: DateTime.UtcNow.Add(options.Value.Expires),
            claims: claims,
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.SecretKey)),
                SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
    }
    
}