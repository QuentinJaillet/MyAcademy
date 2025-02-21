namespace MyAcademy.Course.Infrastructure.Entities;

public class Lesson
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string VideoUrl { get; set; }
    public int Order { get; set; }
    public Chapter Chapter { get; set; }
}