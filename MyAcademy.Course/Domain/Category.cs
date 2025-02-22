namespace MyAcademy.Course.Domain;

public class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ICollection<Course> Categories { get; set; }
}