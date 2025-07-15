namespace BlazorAuthenticationTutorial.Shared.Models;

public class Authentication
{
    public string SecretKey { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
}