using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace MyAcademy.Services;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private ClaimsPrincipal _user = new(new ClaimsIdentity());
    private readonly HttpClient _httpClient;
    private ILogger<CustomAuthStateProvider> _logger;

    public CustomAuthStateProvider(HttpClient httpClient, ILogger<CustomAuthStateProvider> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task NotifyUserAuthentication(UserInfoResponse? user)
    {
        var identity = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        }, "serverAuth");

        _user = new ClaimsPrincipal(identity);

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_user)));
    }

    public void NotifyUserLogout()
    {
        _user = new ClaimsPrincipal(new ClaimsIdentity());
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_user)));
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            _logger.LogInformation("Fetching user authentication state...");
            var response = await _httpClient.GetAsync("/me");

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning($"Authentication check failed: {response.StatusCode}");
                _user = new ClaimsPrincipal(new ClaimsIdentity()); // Utilisateur non authentifié
            }
            else
            {
                var user = await response.Content.ReadFromJsonAsync<UserInfoResponse>();
                var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.Role, user.Role)
                }, "serverAuth");

                _user = new ClaimsPrincipal(identity);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error during authentication check: {ex.Message}");
            _user = new ClaimsPrincipal(new ClaimsIdentity());
        }

        return new AuthenticationState(_user);
    }
}