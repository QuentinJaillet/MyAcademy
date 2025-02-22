namespace MyAcademy.Course.Domain;

public class Chapter
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public int Order { get; set; }
    public Course Course { get; set; }
    public ICollection<Lesson> Lessons { get; set; }
}