using System.ComponentModel.DataAnnotations;

namespace MyAcademy.Components.Pages.Register.Models;

public sealed class RegisterModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
        
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
        
    [Required]
    [DataType(DataType.Password)]
    [Compare(nameof(Password))]
    public string ConfirmPassword { get; set; }
}