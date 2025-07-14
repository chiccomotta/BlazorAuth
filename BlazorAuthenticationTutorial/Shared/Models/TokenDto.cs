using System.ComponentModel.DataAnnotations;

namespace BlazorAuthenticationTutorial.Shared.Models;

public class TokenDto
{
    public string Token { get; set; }
}

public class LoginModel
{
    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; }
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
}
    
public class TokenResponse : TokenDto
{
}

public class Authentication
{
    public string SecretKey { get; set; }
}
