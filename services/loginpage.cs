using System;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class loginpage
    {
        private readonly IConfiguration _configuration;

        public loginpage(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IActionResult> Login(HttpContext httpContext, LoginRequest loginRequest)
        {
            if (loginRequest.Username == "admin" && loginRequest.Password == "admin123")
            {
                var token = GenerateJwtToken(loginRequest.Username);
                return new OkObjectResult(new { Token = token });
            }
            else
            {
                return new UnauthorizedResult();
            }
        }

        public async Task<IActionResult> Logout(HttpContext httpContext)
        {
            // Invalidate the token (e.g., by removing it from a store or marking it as invalid)
            return new OkObjectResult(new { Message = "Successfully logged out" });
        }

        private string GenerateJwtToken(string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["jwt_config:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["jwt_config:Issuer"],
                Audience = _configuration["jwt_config:Audience"]
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }

    public class LoginRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
