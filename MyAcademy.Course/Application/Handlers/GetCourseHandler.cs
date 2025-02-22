using MediatR;
using MyAcademy.Course.Application.Queries;

namespace MyAcademy.Course.Application.Handlers;

public class GetCourseHandler : IRequestHandler<GetCourseQuery, Domain.Course>
{
    public Task<Domain.Course> Handle(GetCourseQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}