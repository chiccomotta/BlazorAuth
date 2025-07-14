using Blazored.LocalStorage;
using System.Net.Http.Headers;

namespace Blazor.App.Authentication;

public static class HttpClientExtensions
{
    public static async Task<HttpResponseMessage> GetAuthenticatedAsync(this HttpClient client, ILocalStorageService localStorage, string requestUri)
    {
        // Leggi il token dal localStorage
        var token = await localStorage.GetItemAsync<string>("authToken");
        
        if (!string.IsNullOrEmpty(token))
        {
            // Imposta l'header Authorization con il Bearer token
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        
        // Esegui la richiesta GET
        return await client.GetAsync(requestUri);
    }
}