using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MyAcademy.Components.Pages.Register.Models;
using MyAcademy.Services;

namespace MyAcademy.Components.Pages.Register;

public partial class Register : ComponentBase
{
    [Inject]
    public AuthService AuthService { get; set; }
    
    [SupplyParameterFromForm] 
    private RegisterModel? Model { get; set; } = new();

    private async Task Submit(EditContext arg)
    {
        Console.WriteLine("Submit");

        //await AuthService.Register(Model.Email, Model.Password);
    }
}