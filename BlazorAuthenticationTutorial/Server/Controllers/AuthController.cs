using BlazorAuthenticationTutorial.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BlazorAuthenticationTutorial.Shared.Models;
using Microsoft.AspNetCore.Authorization;

namespace BlazorAuthenticationTutorial.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<TokenDto>> Login([FromBody] UserLoginDto request)
    {
        var token = GenerateJwtToken();
        return new TokenDto
        {
            Token = token
        };
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
            new Claim(ClaimTypes.Role, "Administrator"),   // Nome dell'utente
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

    [Authorize]
    [HttpGet]
    [Route("secret")]
    public Task<IActionResult> Secret()
    {
        return Task.FromResult<IActionResult>(Ok("This is a secret endpoint! You must be authenticated to access it."));
    }

}