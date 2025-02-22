namespace MyAcademy.Course.Infrastructure.Repositories;

public interface ICourseReadOnlyRepository
{
    Task<Domain.Course?> GetFullCourse(Guid id);
    Task<IEnumerable<Domain.Course>> GetFullCourses();
}