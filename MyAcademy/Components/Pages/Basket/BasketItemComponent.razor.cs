using Microsoft.AspNetCore.Components;
using MyAcademy.Models;

namespace MyAcademy.Components.Pages.Basket;

public partial class BasketItemComponent : ComponentBase
{
    [Parameter]
    public BasketItem? Item { get; set; }
    
    [Parameter] 
    public EventCallback<BasketItem> OnRemove { get; set; }
    
    private void OnRemoveCallback()
    {
        OnRemove.InvokeAsync(Item);
    }
}