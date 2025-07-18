using Blazor.Api.Models.Cosmos;
using Blazor.Api.Services;
using BlazorAuthenticationTutorial.Shared;
using BlazorAuthenticationTutorial.Shared.Models;
using Bogus;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Blazor.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly Authentication authConfig;
    private readonly ICosmosDbService cosmos;

    public AuthController(IOptions<Authentication> _authConfig, ICosmosDbService _cosmos)
    {
        cosmos = _cosmos;
        authConfig = _authConfig.Value;
    }

    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<TokenDto>> Login([FromBody] UserLoginDto request)
    {
        if (request.Username == "chicco" && request.Password == "motta")
        {
            var token = GenerateJwtToken();
            return new TokenDto
            {
                Token = token
            };
        }
        else
        {
            return Unauthorized();
        }
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
            ValidIssuer = authConfig.Issuer,        
            ValidAudience = authConfig.Audience,    
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authConfig.SecretKey)) 
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
        //var response = await cosmos.AddItemAsync<User>(item, item.City);
        return Ok("Item created");
    }

    
    [HttpPost]
    [Route("feed")]
    public async Task<IActionResult> Feed()
    {
        // Configura il Faker per la classe User
        var userFaker = new Faker<User>()
            .RuleFor(u => u.Id, f => Guid.NewGuid().ToString()) // Genera un GUID unico
            .RuleFor(u => u.FirstName, f => f.Name.FirstName()) // Nome fittizio
            .RuleFor(u => u.LastName, f => f.Name.LastName()) // Cognome fittizio
            .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName)) // Email basata su nome e cognome
            .RuleFor(u => u.CreatedAt, f => f.Date.Past(5)) // Data casuale negli ultimi 5 anni
            .RuleFor(u => u.City, f => f.Address.City());      // Città fittizia

        // Genera 100 utenti
        var users = userFaker.Generate(5);
        await cosmos.AddItemsAsync<User>(users);
        return Ok("Items created");
    }

    public string GenerateJwtToken()
    {
        // Definizione dei claims
        var claims = new[]
        {
            new Claim(ClaimTypes.Role, "Admin"),   
        };

        // Chiave segreta per firmare il token
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authConfig.SecretKey));

        // Credenziali di firma
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // Creazione del token
        var token = new JwtSecurityToken(
            issuer: authConfig.Issuer,                  // Emittente del token
            audience: authConfig.Audience,              // Destinatario del token
            claims: claims,                             // Claims inclusi nel token
            expires: DateTime.UtcNow.AddMinutes(30),    // Scadenza del token
            signingCredentials: credentials             // Credenziali di firma
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