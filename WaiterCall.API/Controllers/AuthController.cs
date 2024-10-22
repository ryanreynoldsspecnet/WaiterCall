using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WaiterCall.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private static Dictionary<string, string> _users = new Dictionary<string, string>(); // This is just a demo. Use a database in production.

        [HttpPost("register")]
        public IActionResult Register(string username, string password)
        {
            // Check if the user already exists
            if (_users.ContainsKey(username))
            {
                return BadRequest("User already exists");
            }

            // Register the new user (store username and hashed password)
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            _users.Add(username, hashedPassword);

            return Ok("User registered successfully");
        }

        [HttpPost("login")]
        public IActionResult Login(string username, string password)
        {
            // Validate if user exists and password is correct
            if (_users.TryGetValue(username, out var storedPasswordHash) && BCrypt.Net.BCrypt.Verify(password, storedPasswordHash))
            {
                // Create JWT token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes("your-very-strong-secret-key-of-16-or-more-characters");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.NameIdentifier, username)
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                    Issuer = "your-issuer",
                    Audience = "your-audience"
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return Ok(new { Token = tokenString });
            }

            return Unauthorized("Invalid username or password");
        }
    }
}
