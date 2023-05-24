using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace backend.Models.Utilities;

public class TokenHandler
{
    public string Token { get; set; } = string.Empty;
    public bool UserValidated { get; set; } = false;
    public IConfiguration _config;
    public TokenHandler(string name, string email, IConfiguration configuration)
    {
        _config = configuration;
        CreateToken(name, email);
    }

    public TokenHandler(string token, IConfiguration configuration)
    {
        _config = configuration;
        ValidateToken(token);
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

    public void ValidateToken(string token)
    {
        if (token == null || token.Length == 0)
        {
            UserValidated = false;
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Settings:Token").Value));

        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateIssuer = false,
                ValidateAudience = false,

                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var user = jwtToken.Claims.First(x => x.Type == ClaimTypes.Name).Value;

            if (user == null || user.Length == 0)
            {
                UserValidated = false;
            }
            else
            {
                UserValidated = true;
            }
        }
        catch
        {

            UserValidated = false;
        }
    }
}
