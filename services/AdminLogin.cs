using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

public class AdminLogin
{
    private readonly IConfiguration _configuration;

    public AdminLogin(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<string> Loginpage(string username, string password)
    {
        // Replace with your actual authentication logic
        if (username == "admin" && password == "password")
        {
            var token = GenerateJwtToken(username);
            return token;
        }
        else
        {
            throw new UnauthorizedAccessException("Invalid username or password");
        }
    }

    public Task Logout(HttpContext httpContext)
    {
        // Implement logout logic if needed (e.g., clearing session data)
        return Task.CompletedTask;
    }

    private string GenerateJwtToken(string username)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["JwtConfig:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username)
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}