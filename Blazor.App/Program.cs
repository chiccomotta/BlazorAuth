using Blazor.App;
using Blazor.App.Authentication;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorage();

// Configura l'HttpClientfactory per comunicare con la tua API
builder.Services.AddHttpClient("BackendAPI", client =>
{
    client.BaseAddress = new Uri("https://localhost:44373/"); // URL della tua API
});

// test

// Configura l'autenticazione basata su JWT
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<JwtAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<JwtAuthenticationStateProvider>());

await builder.Build().RunAsync();
