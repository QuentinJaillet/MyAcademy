using System.Security.Claims;
using MediatR;
using MyAcademy.Course.Application.Commands;
using MyAcademy.Course.Infrastructure.Repositories;

namespace MyAcademy.Course.Application.Handlers;

public class CreateCourseHandler : IRequestHandler<CreateCourseCommand, Guid>
{
    private readonly ILogger<CreateCourseHandler> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ICourseRepository _courseRepository;

    public CreateCourseHandler(ILogger<CreateCourseHandler> logger, IHttpContextAccessor httpContextAccessor,
        ICourseRepository courseRepository)
    {
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
        _courseRepository = courseRepository;
    }

    public async Task<Guid> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId is null)
            throw new UnauthorizedAccessException("User ID not found in token.");

        _logger.LogInformation("Create Course: {Title}", request.Title);

        var course = new Domain.Course
        {
            Title = request.Title,
            Subtitle = request.Subtitle,
            Description = request.Description,
            ImageUrl = request.ImageUrl,
            CreatorId = Guid.Parse(userId),
            CategoryId = request.CategoryId,
            /*Tags = request.Tags.Select(tag => new Domain.Tag
            {
                Name = tag
            }).ToList(),*/
            /*Chapters = request.Chapters.Select(chapter => new Domain.Chapter
            {
                Title = chapter.Title,
                Order = chapter.Order,
                Lessons = chapter.Lessons.Select(lesson => new Domain.Lesson
                {
                    Title = lesson.Title,
                    VideoUrl = lesson.VideoUrl,
                    Order = lesson.Order
                }).ToList()
            }).ToList()*/
        };

        await _courseRepository
            .AddCourseAsync(course, cancellationToken)
            .ConfigureAwait(false);

        return course.Id;
    }
}