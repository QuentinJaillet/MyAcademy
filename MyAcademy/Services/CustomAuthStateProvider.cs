using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace MyAcademy.Services;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly ProtectedSessionStorage _protectedSessionStorage;
    private ClaimsPrincipal _user = new(new ClaimsIdentity());
    
    public async Task NotifyUserAuthentication(string token)
    {
        var identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
        _user = new ClaimsPrincipal(identity);
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_user)));
    }

    public async Task NotifyUserLogout()
    {
        _user = new ClaimsPrincipal(new ClaimsIdentity());
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_user)));
    }
    
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var result = await _protectedSessionStorage.GetAsync<string>("authToken");
        if (!result.Success || string.IsNullOrEmpty(result.Value))
        {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        var identity = new ClaimsIdentity(ParseClaimsFromJwt(result.Value), "jwt");
        var user = new ClaimsPrincipal(identity);

        return new AuthenticationState(user);
    }
    
    private IEnumerable<Claim> ParseClaimsFromJwt(string token)
    {
        var handler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(token);
        return jwt.Claims;
    }
}