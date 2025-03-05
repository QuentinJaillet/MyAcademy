using Microsoft.AspNetCore.Components.Authorization;
using MyAcademy.Components;
using MyAcademy.Services;

var builder = WebApplication.CreateBuilder(args);

var cookieContainer = new System.Net.CookieContainer();
var sharedHandler = new HttpClientHandler
{
    UseCookies = true,
    CookieContainer = cookieContainer,
    AllowAutoRedirect = true
};

// Ajouter le service d’authentification
builder.Services.AddAuthorizationCore();
builder.Services.AddHttpClient<CustomAuthStateProvider>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7084/");
}).ConfigurePrimaryHttpMessageHandler(() => sharedHandler);

builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<CustomAuthStateProvider>());

builder.Services.AddHttpClient<AuthService>(options => { options.BaseAddress = new Uri("https://localhost:7084"); })
    .ConfigurePrimaryHttpMessageHandler(() => sharedHandler);

builder.Services.AddHttpClient<CourseService>(options => { options.BaseAddress = new Uri("https://localhost:7084"); })
    .ConfigurePrimaryHttpMessageHandler(() => sharedHandler);

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