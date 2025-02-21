using Microsoft.AspNetCore.Components.Authorization;
using MyAcademy.Components;
using MyAcademy.Services;

var builder = WebApplication.CreateBuilder(args);

// Ajouter HttpClient pour communiquer avec l’API
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7018/") });

// Ajouter le service d’authentification
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
//builder.Services.AddProtectedSessionStorage();

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


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();