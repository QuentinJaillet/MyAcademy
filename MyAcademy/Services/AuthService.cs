using Microsoft.AspNetCore.Components.Authorization;

namespace MyAcademy.Services;

public class AuthService
{
    private readonly HttpClient _httpClient;
    private readonly AuthenticationStateProvider _authStateProvider;

    public AuthService(HttpClient httpClient, AuthenticationStateProvider authStateProvider)
    {
        _httpClient = httpClient;
        _authStateProvider = authStateProvider;
    }

    public async Task<bool> Login(string email, string password)
    {
        var response = await _httpClient.PostAsJsonAsync("https://localhost:7001/login", new { email, password });

        if (!response.IsSuccessStatusCode)
            return false;
        
        var user = await _httpClient.GetFromJsonAsync<UserInfoResponse>("https://localhost:7001/me");

        await ((CustomAuthStateProvider)_authStateProvider).NotifyUserAuthentication(user);
        return true;
    }

    public async Task Logout()
    {
        await _httpClient.PostAsync("https://localhost:7001/logout", null);
        ((CustomAuthStateProvider)_authStateProvider).NotifyUserLogout();
    }
}

public record UserInfoResponse(string Id, string Email, string Role);