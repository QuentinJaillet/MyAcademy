using Microsoft.EntityFrameworkCore;
using MyAcademy.Course.Domain;
using MyAcademy.Course.Infrastructure.Persistence;

namespace MyAcademy.Course.Infrastructure.Repositories;

public class CourseReadOnlyRepository : ICourseReadOnlyRepository
{
    private readonly ApplicationDbContext _dbContext;

    public CourseReadOnlyRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Domain.Course?> GetFullCourse(Guid id)
    {
        return await _dbContext.Courses
            .AsNoTracking()
            .Where(s => s.Id == id)
            .Select(s => new Domain.Course
            {
                Id = s.Id,
                Title = s.Title,
                Subtitle = s.Subtitle,
                Description = s.Description,
                ImageUrl = s.ImageUrl,
                CreatedAt = s.CreatedAt,
                UpdatedAt = s.UpdatedAt,
                Creator = new User
                {
                    Id = s.Creator.Id,
                    FullName = s.Creator.FullName
                },
                Category = new Category
                {
                    Id = s.Category.Id,
                    Name = s.Category.Name
                },
                Chapters = s.Chapters.Select(c => new Chapter
                {
                    Id = c.Id,
                    Title = c.Title,
                    Order = c.Order,
                    Lessons = c.Lessons.Select(l => new Lesson
                    {
                        Id = l.Id,
                        Title = l.Title,
                        VideoUrl = l.VideoUrl,
                        Order = l.Order
                    }).ToList()
                }).ToList(),
                Tags = s.Tags.Select(t => new Domain.Tag
                {
                    Id = t.Id,
                    Name = t.Name
                }).ToList(),
            })
            .FirstOrDefaultAsync()
            .ConfigureAwait(false);
    }
}