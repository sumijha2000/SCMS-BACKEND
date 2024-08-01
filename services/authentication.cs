// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using MySql.Data.MySqlClient;
// using System.Text.Json;
// using System.Net;
// using System.Net.Mail;
// using System.IdentityModel.Tokens.Jwt;
// using System.Security.Claims;
// using Microsoft.IdentityModel.Tokens;
// using System.Text;

// namespace COMMON_PROJECT_STRUCTURE_API.services
// {
//     public class Authentication
//     {
//         private readonly dbServices ds = new dbServices();
//         private readonly string secretKey = "vami czfo yhis ykpr"; // Define your secret key for JWT

//         // Method to handle user signin and generate JWT token
//         public async Task<responseData> SignIn(requestData rData)
//         {
//             responseData resData = new responseData();
//             try
//             {
//                 var email = rData.addInfo["email"].ToString().ToLower();  // Convert email to lowercase
//                 var password = rData.addInfo["password"].ToString();     // Retrieve password
//                 var role = rData.addInfo["role"].ToString(); // Retrieve the selected role

//                 // Check if the user exists and the password matches
//                 var query = @"SELECT * FROM mydb.user_account WHERE email=@EMAIL AND password=@PASSWORD AND role=@ROLE";
//                 MySqlParameter[] myParam = new MySqlParameter[]
//                 {
//                     new MySqlParameter("@EMAIL", email),
//                     new MySqlParameter("@PASSWORD", password),
//                     new MySqlParameter("@ROLE", role)
//                 };
//                 var dbData = ds.executeSQL(query, myParam);
//                 if (dbData[0].Any())
//                 {
//                     // Generate JWT token and return it
//                     var token = GenerateToken(email, role);
//                     resData.rData["rMessage"] = $"Signin Successful. Welcome, {email}!";
//                     resData.rData["TOKEN"] = token;
//                     resData.rData["role"] = role; // Return the role to the client
//                 }
//                 else
//                 {
//                     resData.rData["rMessage"] = "Invalid email, password, or role";
//                 }
//             }
//             catch (Exception ex)
//             {
//                 resData.rData["rMessage"] = "Error: " + ex.Message;
//             }
//             return resData;
//         }

//         // Method to generate JWT token
//         private string GenerateToken(string email, string role)
//         {
//             var tokenHandler = new JwtSecurityTokenHandler();
//             var key = Encoding.ASCII.GetBytes(secretKey);

//             var tokenDescriptor = new SecurityTokenDescriptor
//             {
//                 Subject = new ClaimsIdentity(new Claim[]
//                 {
//                     new Claim(ClaimTypes.Email, email),
//                     new Claim(ClaimTypes.Role, role) // Include role in the token claims
//                 }),
//                 Expires = DateTime.UtcNow.AddHours(1), // Token expires in 1 hour
//                 SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
//             };

//             var token = tokenHandler.CreateToken(tokenDescriptor);
//             return tokenHandler.WriteToken(token);
//         }

//         // Method to handle user logout
//         public async Task<responseData> Logout(requestData rData)
//         {
//             responseData resData = new responseData();
//             try
//             {
//                 var token = rData.addInfo["TOKEN"].ToString();
//                 if (string.IsNullOrEmpty(token))
//                 {
//                     throw new ArgumentException("Token is missing");
//                 }

//                 var tokenHandler = new JwtSecurityTokenHandler();
//                 var key = Encoding.ASCII.GetBytes(secretKey);

//                 // Validate the JWT token
//                 try
//                 {
//                     tokenHandler.ValidateToken(token, new TokenValidationParameters
//                     {
//                         ValidateIssuerSigningKey = true,
//                         IssuerSigningKey = new SymmetricSecurityKey(key),
//                         ValidateIssuer = false,
//                         ValidateAudience = false,
//                         ValidateLifetime = true,
//                         ClockSkew = TimeSpan.Zero
//                     }, out SecurityToken validatedToken);

//                     // Token is valid
//                     resData.rData["rMessage"] = "Logout successful";
//                 }
//                 catch (Exception ex)
//                 {
//                     // Token is invalid or expired
//                     resData.rData["rMessage"] = "Invalid or expired token";
//                     Console.WriteLine($"Exception occurred while validating token: {ex.Message}");
//                 }
//             }
//             catch (Exception ex)
//             {
//                 // General error handling
//                 resData.rData["rMessage"] = "Error: " + ex.Message;
//                 Console.WriteLine($"Exception occurred in Logout method: {ex.Message}");
//             }
//             return resData;
//         }

//         // Method to delete user account
//         public async Task<responseData> DeleteAccount(requestData rData)
//         {
//             responseData resData = new responseData();
//             try
//             {
//                 var userId = rData.addInfo["user_id"].ToString();

//                 // Delete user account
//                 var deleteQuery = @"DELETE FROM mydb.user_account WHERE user_id=@user_id";
//                 MySqlParameter[] deleteParams = new MySqlParameter[]
//                 {
//                     new MySqlParameter("@user_id", userId)
//                 };
//                 var deleteResult = ds.executeSQL(deleteQuery, deleteParams);
//                 resData.rData["rMessage"] = "Account deleted successfully";
//             }
//             catch (Exception ex)
//             {
//                 resData.rData["rMessage"] = "Error: " + ex.Message;
//             }
//             return resData;
//         }
//     }
// }


using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace COMMON_PROJECT_STRUCTURE_API.services
{
    public class Authentication
    {
        private readonly dbServices ds = new dbServices();
        private readonly string secretKey = "vami czfo yhis ykpr"; // Define your secret key for JWT

        // Method to handle user signin and generate JWT token
        public async Task<responseData> SignIn(requestData rData)
        {
            responseData resData = new responseData();
            try
            {
                var email = rData.addInfo["email"].ToString().ToLower();  // Convert email to lowercase
                var password = rData.addInfo["password"].ToString();     // Retrieve password
                var role = rData.addInfo["role"].ToString(); // Retrieve the selected role

                // Hash the provided password
                var hashedPassword = HashPassword(password);

                // Check if the user exists and the password matches
                var query = @"SELECT * FROM mydb.user_account WHERE email=@EMAIL AND password=@PASSWORD AND role=@ROLE";
                MySqlParameter[] myParam = new MySqlParameter[]
                {
                    new MySqlParameter("@EMAIL", email),
                    new MySqlParameter("@PASSWORD", hashedPassword),
                    new MySqlParameter("@ROLE", role)
                };
                var dbData = ds.executeSQL(query, myParam);
                if (dbData[0].Any())
                {
                    // Generate JWT token and return it
                    var token = GenerateToken(email, role);
                    resData.rData["rMessage"] = $"Signin Successful. Welcome, {email}!";
                    resData.rData["TOKEN"] = token;
                    resData.rData["role"] = role; // Return the role to the client
                }
                else
                {
                    resData.rData["rMessage"] = "Invalid email, password, or role";
                }
            }
            catch (Exception ex)
            {
                resData.rData["rMessage"] = "Error: " + ex.Message;
            }
            return resData;
        }

        // Method to hash the password
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    sb.Append(bytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }

        // Method to generate JWT token
        private string GenerateToken(string email, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.Role, role) // Include role in the token claims
                }),
                Expires = DateTime.UtcNow.AddHours(1), // Token expires in 1 hour
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        // Method to handle user logout
        public async Task<responseData> Logout(requestData rData)
        {
            responseData resData = new responseData();
            try
            {
                var token = rData.addInfo["TOKEN"].ToString();
                if (string.IsNullOrEmpty(token))
                {
                    throw new ArgumentException("Token is missing");
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(secretKey);

                // Validate the JWT token
                try
                {
                    tokenHandler.ValidateToken(token, new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    }, out SecurityToken validatedToken);

                    // Token is valid
                    resData.rData["rMessage"] = "Logout successful";
                }
                catch (Exception ex)
                {
                    // Token is invalid or expired
                    resData.rData["rMessage"] = "Invalid or expired token";
                    Console.WriteLine($"Exception occurred while validating token: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                // General error handling
                resData.rData["rMessage"] = "Error: " + ex.Message;
                Console.WriteLine($"Exception occurred in Logout method: {ex.Message}");
            }
            return resData;
        }

        // Method to delete user account
        public async Task<responseData> DeleteAccount(requestData rData)
        {
            responseData resData = new responseData();
            try
            {
                var userId = rData.addInfo["user_id"].ToString();

                // Delete user account
                var deleteQuery = @"DELETE FROM mydb.user_account WHERE user_id=@user_id";
                MySqlParameter[] deleteParams = new MySqlParameter[]
                {
                    new MySqlParameter("@user_id", userId)
                };
                var deleteResult = ds.executeSQL(deleteQuery, deleteParams);
                resData.rData["rMessage"] = "Account deleted successfully";
            }
            catch (Exception ex)
            {
                resData.rData["rMessage"] = "Error: " + ex.Message;
            }
            return resData;
        }
    }
}
