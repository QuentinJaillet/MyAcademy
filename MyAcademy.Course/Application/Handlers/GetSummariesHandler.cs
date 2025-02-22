using MediatR;
using MyAcademy.Course.Application.DTOs;
using MyAcademy.Course.Application.Queries;
using MyAcademy.Course.Infrastructure.Repositories;

namespace MyAcademy.Course.Application.Handlers;

public class GetSummariesHandler : IRequestHandler<GetSummariesQuery, IReadOnlyList<SummaryDto>>
{
    private readonly ILogger<GetSummariesHandler> _logger;
    private readonly ICourseReadOnlyRepository _courseReadOnlyRepository;

    public GetSummariesHandler(ILogger<GetSummariesHandler> logger, ICourseReadOnlyRepository courseReadOnlyRepository)
    {
        _logger = logger;
        _courseReadOnlyRepository = courseReadOnlyRepository;
    }

    public async Task<IReadOnlyList<SummaryDto>> Handle(GetSummariesQuery request, CancellationToken cancellationToken)
    {
        var courses = await _courseReadOnlyRepository
            .GetFullCourses()
            .ConfigureAwait(false);
        
        return courses.Select(course => new SummaryDto
        {
            Id = course.Id,
            Title = course.Title,
            Description = course.Description,
            ImageUrl = course.ImageUrl,
            Creator = course.Creator.FullName
        }).ToList();
    }
}