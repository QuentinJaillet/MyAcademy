namespace MyAcademy.Course.Infrastructure.Entities;

public class Course
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Subtitle { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public User Creator { get; set; }
    public Category Category { get; set; }
    public ICollection<Tag> Tags { get; set; }
    public ICollection<Chapter> Chapters { get; set; }
}