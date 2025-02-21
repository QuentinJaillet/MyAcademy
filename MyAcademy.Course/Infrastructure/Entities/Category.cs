namespace MyAcademy.Course.Infrastructure.Entities;

public class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ICollection<Course> Categories { get; set; }
}