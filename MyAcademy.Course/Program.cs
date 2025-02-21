using MediatR;
using MyAcademy.Course.Application.Queries;

var builder = WebApplication.CreateBuilder(args);

// Ajout de MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/summary", async (IMediator mediator) =>
{
    var query = new GetSummaryCoursesQuery();
    var response = await mediator.Send(query);
    return Results.Ok(response);
}).WithName("Summary").AllowAnonymous();

app.Run();