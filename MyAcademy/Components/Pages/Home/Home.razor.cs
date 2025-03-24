using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MyAcademy.Models;
using MyAcademy.Services;

namespace MyAcademy.Components.Pages.Home;

public partial class Home
{
    [Inject]
    public CourseService CourseService { get; set; }

    [Inject]
    public ILogger<Home> Logger { get; set; }

    public string ErrorMessage { get; set; } = string.Empty;
    public IEnumerable<Course> Summaries { get; set; } = [];
    public IEnumerable<Category>? Categories { get; set; } = [];
    public string SearchTerm { get; set; }

    protected override async Task OnInitializedAsync()
    {
        try
        {
            Categories = await CourseService.GetCategoriesAsync().ConfigureAwait(false);
        }
        catch (Exception e)
        {
            Logger.LogError(e, "Error de chargement des catégories");
            //ErrorMessage = "Une erreur est survenue lors du chargement des cours.";
        }

        try
        {
            //var summaries = await CourseService.GetCoursesAsync();
            Summaries = new List<Course>
            {
                new Course
                {
                    Id = Guid.NewGuid(),
                    Title = "Lorem ipsum dolor sit amet",
                    Author = "Quentin Jaillet",
                    Duration = "12h",
                    Price = 100,
                    Description =
                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed viverra ac justo semper varius. Aliquam pharetra dolor eget euismod pulvinar. In a vehicula quam. Donec et volutpat orci. Aliquam id enim tortor. Proin pulvinar convallis lorem id accumsan. Duis justo felis, semper quis sem sed, varius fermentum arcu.",
                    //Category = new Category { Id = 1, Name = "Catégorie 1" }
                },
                new Course
                {
                    Id = Guid.NewGuid(),
                    Title = "Lorem ipsum dolor sit amet",
                    Author = "Quentin Jaillet",
                    Duration = "12h",
                    Price = 130,
                    Description =
                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed viverra ac justo semper varius. Aliquam pharetra dolor eget euismod pulvinar. In a vehicula quam. Donec et volutpat orci. Aliquam id enim tortor. Proin pulvinar convallis lorem id accumsan. Duis justo felis, semper quis sem sed, varius fermentum arcu.",
                    //Category = new Category { Id = 1, Name = "Catégorie 1" }
                },
                new Course
                {
                    Id = Guid.NewGuid(),
                    Title = "Lorem ipsum dolor sit amet",
                    Author = "Quentin Jaillet",
                    Duration = "12h",
                    Price = 130,
                    Description =
                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed viverra ac justo semper varius. Aliquam pharetra dolor eget euismod pulvinar. In a vehicula quam. Donec et volutpat orci. Aliquam id enim tortor. Proin pulvinar convallis lorem id accumsan. Duis justo felis, semper quis sem sed, varius fermentum arcu.",
                    //Category = new Category { Id = 1, Name = "Catégorie 1" }
                },
                new Course
                {
                    Id = Guid.NewGuid(),
                    Title = "Lorem ipsum dolor sit amet",
                    Author = "Quentin Jaillet",
                    Duration = "12h",
                    Price = 130,
                    Description =
                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed viverra ac justo semper varius. Aliquam pharetra dolor eget euismod pulvinar. In a vehicula quam. Donec et volutpat orci. Aliquam id enim tortor. Proin pulvinar convallis lorem id accumsan. Duis justo felis, semper quis sem sed, varius fermentum arcu.",
                    //Category = new Category { Id = 1, Name = "Catégorie 1" }
                },
                new Course
                {
                    Id = Guid.NewGuid(),
                    Title = "Lorem ipsum dolor sit amet",
                    Author = "Quentin Jaillet",
                    Duration = "12h",
                    Price = 130,
                    Description =
                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed viverra ac justo semper varius. Aliquam pharetra dolor eget euismod pulvinar. In a vehicula quam. Donec et volutpat orci. Aliquam id enim tortor. Proin pulvinar convallis lorem id accumsan. Duis justo felis, semper quis sem sed, varius fermentum arcu.",
                    //Category = new Category { Id = 1, Name = "Catégorie 1" }
                },
                new Course
                {
                    Id = Guid.NewGuid(),
                    Title = "Lorem ipsum dolor sit amet",
                    Author = "Quentin Jaillet",
                    Duration = "12h",
                    Price = 130,
                    Description =
                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed viverra ac justo semper varius. Aliquam pharetra dolor eget euismod pulvinar. In a vehicula quam. Donec et volutpat orci. Aliquam id enim tortor. Proin pulvinar convallis lorem id accumsan. Duis justo felis, semper quis sem sed, varius fermentum arcu.",
                    //Category = new Category { Id = 1, Name = "Catégorie 1" }
                },
                new Course
                {
                    Id = Guid.NewGuid(),
                    Title = "Lorem ipsum dolor sit amet",
                    Author = "Quentin Jaillet",
                    Duration = "12h",
                    Price = 130,
                    Description =
                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed viverra ac justo semper varius. Aliquam pharetra dolor eget euismod pulvinar. In a vehicula quam. Donec et volutpat orci. Aliquam id enim tortor. Proin pulvinar convallis lorem id accumsan. Duis justo felis, semper quis sem sed, varius fermentum arcu.",
                    //Category = new Category { Id = 1, Name = "Catégorie 1" }
                },
                new Course
                {
                    Id = Guid.NewGuid(),
                    Title = "Lorem ipsum dolor sit amet",
                    Author = "Quentin Jaillet",
                    Duration = "12h",
                    Price = 130,
                    Description =
                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed viverra ac justo semper varius. Aliquam pharetra dolor eget euismod pulvinar. In a vehicula quam. Donec et volutpat orci. Aliquam id enim tortor. Proin pulvinar convallis lorem id accumsan. Duis justo felis, semper quis sem sed, varius fermentum arcu.",
                    //Category = new Category { Id = 1, Name = "Catégorie 1" }
                },
            };

            /*if (summaries is not null)
            {
                Summaries = summaries;
            }
            else
            {
                ErrorMessage = "Aucun cours n'a été trouvé.";
            }*/
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
        //CategoriesSelected[category] = !CategoriesSelected[category];

        Filter();

        return Task.CompletedTask;
    }

    private void Filter()
    {
        // Récupérère les catégories sélectionnéesz
        /*var selectedCategories = CategoriesSelected
            .Where(x => x.Value)
            .Select(x => x.Key)
            .ToList();*/

        //Console.WriteLine($"Selected categories: {string.Join(", ", selectedCategories.Select(x => x.Name))}");
    }

    private void HandleSelectionChanged(List<Category> selectedCategories)
    {
        // Affiche les sélection dans la console
        Console.WriteLine($"Selected categories: {string.Join(", ", selectedCategories.Select(x => x.Name))}");
    }
}