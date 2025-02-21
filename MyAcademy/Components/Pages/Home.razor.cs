using Microsoft.AspNetCore.Components;
using MyAcademy.Services;

namespace MyAcademy.Components.Pages;

public partial class Home
{
    [Inject]
    public CourseService CourseService { get; set; }
    
    [Inject]
    public ILogger<Home> Logger { get; set; }
    
    public string ErrorMessage { get; set; } = string.Empty;
    public IEnumerable<CourseSummary> Summaries { get; set; } = [];

    protected override async Task OnInitializedAsync()
    {
        try
        {
            var summaries = await CourseService.GetSummariesAsync();
            
            if (summaries is not null)
            {
                Summaries = summaries;
            }
            else
            {
                ErrorMessage = "Aucun cours n'a été trouvé.";
            }
        }
        catch (Exception e)
        {
            Logger.LogError(e, "error de chargement des cours");
            ErrorMessage = "Une erreur est survenue lors du chargement des cours.";
        }
    }
}