namespace MyAcademy.Services;

public class AuthService
{
    private readonly HttpClient _httpClient;
    private readonly CustomAuthStateProvider _authStateProvider;

    public AuthService(HttpClient httpClient, CustomAuthStateProvider authStateProvider)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _authStateProvider = authStateProvider ?? throw new ArgumentNullException(nameof(authStateProvider));
    }

    public async Task<bool> Login(string email, string password)
    {
        var response = await _httpClient.PostAsJsonAsync("/login", new { email, password });

        if (!response.IsSuccessStatusCode)
            return false;

        var user = await _httpClient.GetFromJsonAsync<UserInfoResponse>("/me");

        await _authStateProvider.NotifyUserAuthentication(user);

        return true;
    }

    public async Task Logout()
    {
        await _httpClient.PostAsync("/logout", null);
        _authStateProvider.NotifyUserLogout();
    }

    public async Task<bool> Register(string email, string password)
    {
        var response = await _httpClient.PostAsJsonAsync("/register", new { email, password });

        return response.IsSuccessStatusCode;
    }
}

public record UserInfoResponse(string Id, string Email, string Role);