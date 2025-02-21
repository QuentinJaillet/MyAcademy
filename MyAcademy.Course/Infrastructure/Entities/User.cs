namespace MyAcademy.Course.Infrastructure.Entities;

public class User
{
    public Guid Id { get; set; }
    
    public string FullName { get; set; }
    
    public ICollection<Course> Courses { get; set; }
}