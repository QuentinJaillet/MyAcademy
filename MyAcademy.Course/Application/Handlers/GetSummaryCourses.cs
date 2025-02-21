using MediatR;
using MyAcademy.Course.Application.Queries;
using MyAcademy.Course.Domain;

namespace MyAcademy.Course.Application.Handlers;

public class GetSummaryCourses : IRequestHandler<GetSummaryCoursesQuery, IReadOnlyList<SummaryCours>>
{
    public Task<IReadOnlyList<SummaryCours>> Handle(GetSummaryCoursesQuery request, CancellationToken cancellationToken)
    {
        var courses = new List<SummaryCours>()
        {
            new SummaryCours
            {
                Id = Guid.NewGuid(), Title = "Course 1", Description = "Description 1",
                ImageUrl = "https://via.placeholder.com/150"
            },
            new SummaryCours
            {
                Id = Guid.NewGuid(), Title = "Course 2", Description = "Description 2",
                ImageUrl = "https://via.placeholder.com/150"
            },
            new SummaryCours
            {
                Id = Guid.NewGuid(), Title = "Course 3", Description = "Description 3",
                ImageUrl = "https://via.placeholder.com/150"
            }
        };

        return Task.FromResult<IReadOnlyList<SummaryCours>>(courses);
    }
}