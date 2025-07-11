using BlazorAuthenticationTutorial.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlazorAuthenticationTutorial.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<string>> Login(UserLoginDto request)
        {
            //string token = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiVG9ueSBTdGFyayIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6Iklyb24gTWFuIiwiZXhwIjozMTY4NTQwMDAwfQ.IbVQa1lNYYOzwso69xYfsMOHnQfO3VLvVqV2SOXS7sTtyyZ8DEf5jmmwz2FGLJJvZnQKZuieHnmHkg7CGkDbvA";

            var token = GenerateJwtToken();
            
            return token;
        }


        [HttpGet("validate")]
        public IActionResult ValidateToken([FromQuery] string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return BadRequest("Token is required.");
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "https://your-issuer.com", // Sostituisci con il tuo issuer
                ValidAudience = "https://your-audience.com", // Sostituisci con il tuo audience
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your-256-bit-secret-your-256-bit-secret-your-256-bit-secret-")) // Chiave segreta
            };
            try
            {
                // Valida il token
                var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                // Controlla che il token sia un JWT
                if (validatedToken is JwtSecurityToken jwtToken)
                {
                    // Restituisce i claims del token
                    var claims = principal.Claims.Select(c => new { c.Type, c.Value });
                    return Ok(new
                    {
                        Message = "Token is valid.",
                        Claims = claims
                    });
                }
                return BadRequest("Invalid token format.");
            }
            catch (SecurityTokenException ex)
            {
                return Unauthorized(new { Message = "Token validation failed.", Error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while validating the token.", Error = ex.Message });
            }
        }


        [HttpGet]
        [Route("hello")]
        public async Task<IActionResult> Hello()
        {
            return Ok("Hello from the API!");
        }


        public static string GenerateJwtToken()
        {
            // Definizione dei claims
            var claims = new[]
            {
                new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", "Tony Stark"), // Nome dell'utente
            };
            
            // Chiave segreta per firmare il token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your-256-bit-secret-your-256-bit-secret-your-256-bit-secret-"));
            
            // Credenziali di firma
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            
            // Creazione del token
            var token = new JwtSecurityToken(
                issuer: "https://your-issuer.com", // Emittente del token
                audience: "https://your-audience.com", // Destinatario del token
                claims: claims, // Claims inclusi nel token
                expires: DateTime.UtcNow.AddMinutes(30), // Scadenza del token
                signingCredentials: credentials // Credenziali di firma
            );
            
            // Restituzione del token come stringa
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

}
    
}
