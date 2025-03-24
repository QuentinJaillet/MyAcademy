using System.ComponentModel.DataAnnotations;

namespace MyAcademy.BFFWeb.Models;

public class Register
{
    [Required]
    public required string Email { get; set; }

    [Required]
    public required string Password { get; set; }
}