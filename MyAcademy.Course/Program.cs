using MediatR;
using Microsoft.EntityFrameworkCore;
using MyAcademy.Course.Application.Queries;
using MyAcademy.Course.Infrastructure.Persistence;
using MyAcademy.Course.Infrastructure.Repositories;
using MyAcademy.Course.Mappers;
using MyAcademy.Course.Models;

var builder = WebApplication.CreateBuilder(args);

// Ajout d'Entity Framework avec Identity
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Ajout de MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICourseReadOnlyRepository, CourseReadOnlyRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();

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

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/summaries", async (IMediator mediator) =>
{
    var query = new GetSummariesQuery();
    var response = await mediator.Send(query);
    return Results.Ok(response);
}).WithName("Summaries").AllowAnonymous();

app.MapGet("/courses/{id}", async (IMediator mediator, Guid id) =>
{
    var query = new GetCourseQuery(id);
    var response = await mediator.Send(query);
    return response is not null ? Results.Ok(response) : Results.NotFound();
}).WithName("Course").AllowAnonymous();

// Ajout d'un cours 
app.MapPost("/courses", async (IMediator mediator, CreateCourseRequest request) =>
    {
        var response = await mediator.Send(request.ToCommand());
        return Results.Created($"/courses/{response}", response);
    })
    .WithName("CreateCourse")
    .RequireAuthorization();

app.Run();