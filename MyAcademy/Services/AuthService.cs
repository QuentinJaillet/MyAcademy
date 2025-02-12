using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace MyAcademy.Services;

public class AuthService : AuthenticationStateProvider
{
    private readonly HttpClient _httpClient;
    private ClaimsPrincipal _user = new(new ClaimsIdentity());

    public AuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        return Task.FromResult(new AuthenticationState(_user));
    }
    
    public async Task<bool> Login(string email, string password)
    {
        var response = await _httpClient.PostAsJsonAsync("https://localhost:7018/login", new { email, password });

        if (!response.IsSuccessStatusCode)
            return false;

        var result = await response.Content.ReadFromJsonAsync<string>();
        if (result == null || string.IsNullOrEmpty(result))
            return false;

        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(result);
        var identity = new ClaimsIdentity(token.Claims, "jwt");
        _user = new ClaimsPrincipal(identity);

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_user)));
        return true;
    }

    public void Logout()
    {
        _user = new ClaimsPrincipal(new ClaimsIdentity());
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_user)));
    }

    private class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
    }
}