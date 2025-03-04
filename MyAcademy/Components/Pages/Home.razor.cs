using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MyAcademy.Models;
using MyAcademy.Services;

namespace MyAcademy.Components.Pages;

public partial class Home
{
    [Inject] public CourseService CourseService { get; set; }

    [Inject] public ILogger<Home> Logger { get; set; }

    public string ErrorMessage { get; set; } = string.Empty;
    public IEnumerable<CourseSummary> Summaries { get; set; } = [];

    public IEnumerable<Category> Categories { get; set; } = [];
    public Dictionary<Category, bool> CategoriesSelected { get; set; } = new Dictionary<Category, bool>();
    public string SearchTerm { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Categories = new List<Category>
        {
            new Category { Id = Guid.NewGuid(), Name = "Développement" },
            new Category { Id = Guid.NewGuid(), Name = "Design" },
            new Category { Id = Guid.NewGuid(), Name = "Marketing" },
            new Category { Id = Guid.NewGuid(), Name = "Finance" },
            new Category { Id = Guid.NewGuid(), Name = "Langues" },
            new Category { Id = Guid.NewGuid(), Name = "Autres" }
        };

        foreach (var category in Categories)
        {
            CategoriesSelected.Add(category, false);
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
            Logger.LogError(e, "error de chargement des cours");
            ErrorMessage = "Une erreur est survenue lors du chargement des cours.";
        }
    }

    private void Callback(KeyboardEventArgs obj)
    {
        throw new NotImplementedException();
    }

    private Task FilterByCategory(Category category)
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