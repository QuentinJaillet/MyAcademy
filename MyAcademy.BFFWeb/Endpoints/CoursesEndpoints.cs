namespace MyAcademy.BFFWeb.Endpoints;

public static class CoursesEndpoints
{
    public static RouteGroupBuilder MapCoursesEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/courses");

        group.MapGet("/", () =>
        {
            return Results.Ok(new List<string> { "Alice", "Bob", "Charlie" });
        });

        /*group.MapPost("/", (string name) =>
        {
            return Results.Created($"/users/{name}", new { Name = name });
        });

        group.MapGet("/{id:int}", (int id) =>
        {
            return Results.Ok(new { Id = id, Name = $"User {id}" });
        });*/

        return group;
    }
}