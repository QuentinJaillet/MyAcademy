using System.Net.Http.Headers;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace MyAcademy.Services;

public class AuthService
{
    private readonly HttpClient _httpClient;
    private readonly ProtectedSessionStorage _protectedSessionStorage;
    private readonly AuthenticationStateProvider _authStateProvider;
    private string? _token;

    public AuthService(HttpClient httpClient, ProtectedSessionStorage protectedSessionStorage, AuthenticationStateProvider authStateProvider)
    {
        _httpClient = httpClient;
        _protectedSessionStorage = protectedSessionStorage;
        _authStateProvider = authStateProvider;
    }

    public async Task<bool> Login(string email, string password)
    {
        var response = await _httpClient.PostAsJsonAsync("https://localhost:7018/login", new { email, password });

        if (!response.IsSuccessStatusCode)
            return false;

        var result = await response.Content.ReadFromJsonAsync<string>();
        if (result == null || string.IsNullOrEmpty(result))
            return false;

        // Stocker le token dans Protected Session Storage
        await _protectedSessionStorage.SetAsync("authToken", _token);

        // Configurer HttpClient pour les appels futurs
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

        // Notifier Blazor de l'état de connexion
        await ((CustomAuthStateProvider)_authStateProvider).NotifyUserAuthentication(_token);
        return true;
    }

    public async Task Register(string email, string password)
    {
       var response =  await _httpClient.PostAsJsonAsync("https://localhost:7018/register", new { email, password });

         if (!response.IsSuccessStatusCode)
              throw new Exception("Erreur lors de l'inscription");
    }

    public async Task Logout()
    {
        // appel de l'action de déconnexion
        await _httpClient.PostAsync("https://localhost:7018/logout", null);

        _token = null;
        await _protectedSessionStorage.DeleteAsync("authToken");

        _httpClient.DefaultRequestHeaders.Authorization = null;
         await ((CustomAuthStateProvider)_authStateProvider).NotifyUserLogout();
    }

    private class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
    }
}