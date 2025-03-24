using System.Net.Http.Headers;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace MyAcademy.BFFWeb.Endpoints;

public static class IdentityEndpoints
{
    public static void MapIdentityEndpoints(this WebApplication app)
    {
        app.MapPost("/register",
                async (IHttpClientFactory httpClientFactory, [FromBody] RegisterRequest request) =>
                {
                    app.Logger.LogInformation("Received register request.");

                    var httpClient = httpClientFactory.CreateClient("AuthApi");
                    var response = await httpClient.PostAsJsonAsync("register", request);

                    if (!response.IsSuccessStatusCode)
                        return Results.Unauthorized();

                    return Results.Ok();
                })
            .WithName("Register")
            .AllowAnonymous();

        app.MapPost("/login",
                async (IHttpClientFactory httpClientFactory, HttpContext httpContext,
                    [FromBody] LoginRequest request) =>
                {
                    var httpClient = httpClientFactory.CreateClient("AuthApi");
                    var response = await httpClient.PostAsJsonAsync("login", request);

                    if (!response.IsSuccessStatusCode)
                        return Results.Unauthorized();

                    var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
                    if (result == null || string.IsNullOrEmpty(result.Token))
                        return Results.Unauthorized();

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, result.UserId),
                        new Claim(ClaimTypes.Name, request.Email),
                        new Claim("Token", result.Token) // Stocker le token de manière sécurisée
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                    return Results.Ok();
                })
            .WithName("Login")
            .AllowAnonymous();

        app.MapPost("/logout", async (HttpContext httpContext) =>
            {
                await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return Results.Ok();
            })
            .WithName("Logout");

        app.MapGet("/me", async (IHttpClientFactory httpClientFactory, HttpContext httpContext, HttpClient client) =>
            {
                var user = httpContext.User;
                if (user.Identity is not { IsAuthenticated: true })
                    return Results.Unauthorized();

                var token = user.FindFirst("Token")?.Value;
                if (string.IsNullOrEmpty(token))
                    return Results.Unauthorized();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var httpClient = httpClientFactory.CreateClient("AuthApi");
                var response = await httpClient.GetAsync("me"); // Appel à l'API Identity
                if (!response.IsSuccessStatusCode)
                    return Results.Unauthorized();

                var userInfo = await response.Content.ReadFromJsonAsync<UserInfoResponse>();
                return Results.Ok(userInfo);
            })
            .WithName("Me")
            .RequireAuthorization();
    }
}