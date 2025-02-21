namespace MyAcademy.Course.Infrastructure.Entities;

public class Course
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public string ImageUrl { get; set; }
    
    public User Creator { get; set; }
}