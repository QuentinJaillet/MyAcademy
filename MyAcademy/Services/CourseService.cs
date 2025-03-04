using MyAcademy.Models;

namespace MyAcademy.Services;

public class CourseService
{
    private readonly HttpClient _httpClient;

    public CourseService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<CourseSummary>?> GetCoursesAsync()
    {
        var response = await _httpClient
            .GetFromJsonAsync<IEnumerable<CourseSummary>>("courses")
            .ConfigureAwait(false);

        return response;
    }

    public async Task<IEnumerable<Category>?> GetCategoriesAsync()
    {
        var response = await _httpClient
            .GetFromJsonAsync<IEnumerable<Category>>("categories")
            .ConfigureAwait(false);

        return response;
    }
}

public class CourseSummary
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public string Creator { get; set; }
}