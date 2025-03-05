using MyAcademy.BFFWeb.Models;

namespace MyAcademy.BFFWeb.Endpoints;

public static class CoursesEndpoints
{
    public static RouteGroupBuilder MapCoursesEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/courses");

        group.MapGet("/", async (ILogger<Program> logger, IHttpClientFactory httpClientFactory) =>
        {
            logger.LogInformation("Get courses");

            var httpClient = httpClientFactory.CreateClient("CourseApi");

            try
            {
                var courses = await httpClient
                    .GetFromJsonAsync<IEnumerable<Course>>("courses")
                    .ConfigureAwait(false);

                return Results.Ok(courses);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error when calling CourseApi");
                return Results.Problem("Error when calling CourseApi",
                    statusCode: StatusCodes.Status500InternalServerError,
                    type: "Error");
            }
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