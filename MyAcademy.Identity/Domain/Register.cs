using System.ComponentModel.DataAnnotations;

namespace MyAcademy.Identity.Domain;

public class Register
{
    [Required]
    public required string Email { get; set; }
    
    [Required]
    public required string Password { get; set; }
}