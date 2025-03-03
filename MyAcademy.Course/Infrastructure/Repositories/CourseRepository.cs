using MyAcademy.Course.Infrastructure.Persistence;

namespace MyAcademy.Course.Infrastructure.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly ApplicationDbContext _dbContext;

    public CourseRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Domain.Course> AddCourseAsync(Domain.Course course, CancellationToken cancellationToken)
    {
        await _dbContext.Courses
            .AddAsync(course, cancellationToken)
            .ConfigureAwait(false);

        await _dbContext
            .SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);

        return course;
    }
}