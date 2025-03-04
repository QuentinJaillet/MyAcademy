using Microsoft.AspNetCore.Authentication.Cookies;
using MyAcademy.BFFWeb.Endpoints;

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

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddHttpClient("AuthApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:7018/"); // API d'authentification
});

builder.Services.AddHttpClient("CourseApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:7294/"); // API des cours
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

app.UseSwagger();
app.UseSwaggerUI();

app.MapIdentityEndpoints();
app.MapCoursesEndpoints();
app.MapCategoriesEndpoints();

app.Run();

public record LoginRequest(string Email, string Password);

public record LoginResponse(string Token, string UserId);

public record UserInfoResponse(string Id, string Email, string Role);