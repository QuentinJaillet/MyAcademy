using Microsoft.AspNetCore.Components;
using MyAcademy.Models;

namespace MyAcademy.Components.Pages.Home.Components;

public partial class CategoriesFilter
{
    [Parameter]
    public IEnumerable<Category>? Categories { get; set; }

    [Parameter]
    public EventCallback<List<Category>> OnSelectionChanged { get; set; }

    private List<Category> _selectedCategories = [];

    private async Task HandleCheckboxChange(Category category)
    {
        if (!_selectedCategories.Remove(category))
            _selectedCategories.Add(category);

        await OnSelectionChanged
            .InvokeAsync(_selectedCategories)
            .ConfigureAwait(false);
    }
}