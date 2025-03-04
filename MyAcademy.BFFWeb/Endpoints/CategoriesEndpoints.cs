using MyAcademy.BFFWeb.Models;

namespace MyAcademy.BFFWeb.Endpoints;

public static class CategoriesEndpoints
{
    public static RouteGroupBuilder MapCategoriesEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/categories");

        group.MapGet("/", async (ILogger<Program> logger, IHttpClientFactory httpClientFactory) =>
        {
            logger.LogInformation("Get categories");

            var httpClient = httpClientFactory.CreateClient("CourseApi");

            // Todo gestion du cache ? 

            try
            {
                var categories = await httpClient
                    .GetFromJsonAsync<IEnumerable<Category>>("categories")
                    .ConfigureAwait(false);

                return Results.Ok(categories);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error when calling CourseApi");
                return Results.Problem("Error when calling CourseApi",
                    statusCode: StatusCodes.Status500InternalServerError,
                    type: "Error");
            }
        });

        return group;
    }
}