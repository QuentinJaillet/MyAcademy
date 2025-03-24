using MyAcademy.Models;

namespace MyAcademy.Services;

public class CourseService
{
    private readonly HttpClient _httpClient;

    public CourseService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Course>?> GetCoursesAsync()
    {
        var response = await _httpClient
            .GetFromJsonAsync<IEnumerable<Course>>("courses")
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

public class Course
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Author { get; set; }
    public string Duration { get; set; }
    public decimal Price { get; set; }
}