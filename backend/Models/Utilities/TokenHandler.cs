using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace backend.Models.Utilities;

public class TokenHandler
{
    public string Token { get; set; } = string.Empty;
    public IConfiguration _config;
    public TokenHandler(string name, string email, IConfiguration configuration)
    {
        _config = configuration;
        CreateToken(name, email);
    }

    private void CreateToken(string name, string email)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, name),
            new Claim(ClaimTypes.Email, email)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Settings:Token").Value));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(7),
            signingCredentials: creds
            );

        Token = new JwtSecurityTokenHandler().WriteToken(token);
    }
}
