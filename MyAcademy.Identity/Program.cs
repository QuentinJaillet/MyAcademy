using System.Text;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyAcademy.Identity.Application.Commands;
using MyAcademy.Identity.Application.Queries;
using MyAcademy.Identity.Domain;
using MyAcademy.Identity.Infrastructure;
using MyAcademy.Identity.Infrastructure.Entities;
using MyAcademy.Identity.Models;

var builder = WebApplication.CreateBuilder(args);

// Ajout d'Entity Framework avec Identity
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Ajout de MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Configuration de JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "https://mon-site.com",
            ValidAudience = "https://mon-site.com",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MaSuperCleSecrete123!"))
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Migration automatique au démarrage
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.EnsureCreated(); // Crée la DB si elle n'existe pas
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.MapPost("/login", async (IMediator mediator, Login login) =>
{
    var command = new LoginCommand(login.Email, login.Password);
    var response = await mediator.Send(command);
    return Results.Ok(new LoginResponse(response.Token, response.UserId));
}).WithName("Login");

app.MapPost("/register", async (IMediator mediator, Register register) =>
{
    var command = new RegisterCommand(register.Email, register.Password);
    var response = await mediator.Send(command);
    return response ? Results.NoContent() : Results.BadRequest();
}).WithName("Register");

app.MapGet("/me", async (IMediator mediator) =>
{
    var query = new MeQuery();
    var response = await mediator.Send(query);
    return Results.Ok(new UserInfoResponse(response.Id, response.Email, "User"));
}).WithName("Me").RequireAuthorization();

app.MapPost("/logout", async (IMediator mediator) =>
{
    var command = new LogoutCommand();
    await mediator.Send(command);
    return Results.Ok();
}).WithName("Logout").RequireAuthorization();

app.Run();