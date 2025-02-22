using MediatR;
using MyAcademy.Course.Application.DTOs;
using MyAcademy.Course.Application.Queries;

namespace MyAcademy.Course.Application.Handlers;

public class GetSummaryCoursesHandler : IRequestHandler<GetSummaryCoursesQuery, IReadOnlyList<SummaryDto>>
{
    public Task<IReadOnlyList<SummaryDto>> Handle(GetSummaryCoursesQuery request, CancellationToken cancellationToken)
    {
        var courses = new List<SummaryDto>()
        {
            new SummaryDto
            {
                Id = Guid.NewGuid(), Title = "Course 1", Description = "Description 1",
                ImageUrl = "https://via.placeholder.com/150"
            },
            new SummaryDto
            {
                Id = Guid.NewGuid(), Title = "Course 2", Description = "Description 2",
                ImageUrl = "https://via.placeholder.com/150"
            },
            new SummaryDto
            {
                Id = Guid.NewGuid(), Title = "Course 3", Description = "Description 3",
                ImageUrl = "https://via.placeholder.com/150"
            }
        };

        return Task.FromResult<IReadOnlyList<SummaryDto>>(courses);
    }
}