using System.Text.Json;

namespace MyAcademy.Services;

public class CourseService
{
    private readonly HttpClient _httpClient;

    public CourseService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<IEnumerable<CourseSummary>?> GetSummariesAsync()
    {
        var response = await _httpClient
            .GetFromJsonAsync<IEnumerable<CourseSummary>>("summaries")
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
}