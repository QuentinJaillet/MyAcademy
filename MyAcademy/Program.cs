using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components.Authorization;
using MyAcademy.Components;
using MyAcademy.Services;

var builder = WebApplication.CreateBuilder(args);

// 1️⃣ Ajouter l'authentification
builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme; // Utilisation des cookies
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; // Ou JWT selon ton besoin
    })
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        options.LoginPath = "/login"; // Page de connexion
        options.LogoutPath = "/logout"; // Page de déconnexion
    })
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.Authority = "https://localhost:7018"; // Adresse de ton API d'authentification
        options.Audience = "myacademy"; // Audience définie dans ton API
    });

// Ajouter le service d’authentification
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
//builder.Services.AddProtectedSessionStorage();

builder.Services.AddHttpClient<AuthService>(options =>
{
    options.BaseAddress = new Uri("https://localhost:7018");
});

builder.Services.AddHttpClient<CourseService>(options =>
{
    options.BaseAddress = new Uri("https://localhost:7294");
});

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

// Ajouter l'authentification et l'autorisation
app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();