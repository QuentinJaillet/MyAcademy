using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Ajouter l'authentification avec cookies
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.Strict;
        options.LoginPath = "/login";
        options.LogoutPath = "/logout";
    });

builder.Services.AddAuthorization();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddHttpClient("AuthApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:7018/"); // API d'authentification
});

builder.Services.AddHttpClient("CourseApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:5002/"); // API des cours
});

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

/// 🔐 **Endpoint de login (vérifie et stocke le JWT)**
app.MapPost("/login", async (IHttpClientFactory httpClientFactory, HttpContext httpContext, [FromBody] LoginRequest request) =>
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
});

/// 🚪 **Déconnexion**
app.MapPost("/logout", async (HttpContext httpContext) =>
{
    await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    return Results.Ok();
});



app.Run();

public record LoginRequest(string Email, string Password);
public record LoginResponse(string Token, string UserId);
public record UserInfoResponse(string Id, string Email, string Role);