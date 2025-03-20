using Microsoft.AspNetCore.Components;
using MyAcademy.Models;

namespace MyAcademy.Components.Pages.Home.Components;

public partial class CategoriesFilter
{
    [Parameter]
    public IEnumerable<Category>? Categories { get; set; }
    
    private IDictionary<Models.Category, bool> CategoriesSelected { get; set; } = new Dictionary<Models.Category, bool>();
    
    protected override void OnParametersSet()
    {
        if (Categories != null) 
            CategoriesSelected = Categories.ToDictionary(category => category, _ => false);
    }
    
    private void CheckboxChanged(ChangeEventArgs e)
    {
        // get the checkbox state
        var value = e.Value;
        Console.WriteLine($"Checkbox changed {value}");
        Console.WriteLine(e);
    }
}