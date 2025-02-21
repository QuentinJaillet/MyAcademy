namespace MyAcademy.Course.Infrastructure.Entities;

public class Tag
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ICollection<Course> Courses { get; set; }
}