using System.ComponentModel.DataAnnotations;

namespace MyAcademy.Course.Models;

public record CreateCourseRequest
{
    [Required]
    public string Title { get; init; }
    [Required]
    public string Subtitle { get; init; }
    [Required]
    public string Description { get; init; }
    [Required]
    public string ImageUrl { get; init; }
    [Required]
    public Guid CategoryId { get; init; }
}