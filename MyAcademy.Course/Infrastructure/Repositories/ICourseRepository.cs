namespace MyAcademy.Course.Infrastructure.Repositories;

public interface ICourseRepository
{
    Task<Domain.Course> AddCourseAsync(Domain.Course course, CancellationToken cancellationToken);
}