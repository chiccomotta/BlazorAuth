using BlazorAuthenticationTutorial.Shared.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Leggi il valore di SecretKey dalla configurazione
var secretKey = builder.Configuration["Authentication:SecretKey"];

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
            ValidIssuer = "https://your-issuer.com",
            ValidAudience = "https://your-audience.com",
            RoleClaimType = ClaimTypes.Role // Mappa il claim `ClaimTypes.Role` per i ruoli
        };
    });

builder.Services.Configure<Authentication>(builder.Configuration.GetSection("Authentication"));

// Aggiungi i servizi CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsSettings", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader() // Consenti qualsiasi intestazione
            .AllowAnyMethod(); // Consenti qualsiasi metodo HTTP (GET, POST, ecc.)
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseCors("CorsSettings");
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

// Abilita l'autenticazione e l'autorizzazione
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
