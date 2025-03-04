using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MyAcademy.Services;

namespace MyAcademy.Components.Pages;

public partial class Home
{
    [Inject] public CourseService CourseService { get; set; }

    [Inject] public ILogger<Home> Logger { get; set; }

    public string ErrorMessage { get; set; } = string.Empty;
    public IEnumerable<CourseSummary> Summaries { get; set; } = [];
    public Dictionary<Models.Category, bool> CategoriesSelected { get; set; } = new Dictionary<Models.Category, bool>();
    public string SearchTerm { get; set; }

    protected override async Task OnInitializedAsync()
    {
        try
        {
            var categories = await CourseService.GetCategoriesAsync().ConfigureAwait(false);

            foreach (var category in categories)
            {
                CategoriesSelected.Add(category, false);
            }
        }
        catch (Exception e)
        {
            Logger.LogError(e, "Error de chargement des catégories");
            //ErrorMessage = "Une erreur est survenue lors du chargement des cours.";
        }


        try
        {
            var summaries = await CourseService.GetCoursesAsync();

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
            Logger.LogError(e, "Error de chargement des cours");
            ErrorMessage = "Une erreur est survenue lors du chargement des cours.";
        }
    }

    private void Callback(KeyboardEventArgs obj)
    {
        throw new NotImplementedException();
    }

    private Task FilterByCategory(Models.Category category)
    {
        CategoriesSelected[category] = !CategoriesSelected[category];

        Filter();

        return Task.CompletedTask;
    }

    private void Filter()
    {
        // Récupérère les catégories sélectionnées
        var selectedCategories = CategoriesSelected
            .Where(x => x.Value)
            .Select(x => x.Key)
            .ToList();

        Console.WriteLine($"Selected categories: {string.Join(", ", selectedCategories.Select(x => x.Name))}");
    }
}