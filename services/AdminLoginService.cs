using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class AdminLoginService
    {
        private readonly IConfiguration _configuration;

        // Hardcoded admin credentials
        private const string AdminUsername = "admin";
        private const string AdminPassword = "password123";

        public AdminLoginService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> Authenticate(string username, string password)
        {
            // Validate the user credentials
            if (username == AdminUsername && password == AdminPassword)
            {
                // Generate JWT token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["jwt_config:Key"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, username)
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return await Task.FromResult(tokenHandler.WriteToken(token));
            }
            return null;
        }
       

    }
}

