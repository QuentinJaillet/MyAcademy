using MediatR;
using MyAcademy.Course.Application.Queries;
using MyAcademy.Course.Infrastructure.Repositories;

namespace MyAcademy.Course.Application.Handlers;

public class GetCourseHandler : IRequestHandler<GetCourseQuery, Domain.Course>
{
    private readonly ICourseReadOnlyRepository _courseReadOnlyRepository;
    private readonly ILogger<GetCourseHandler> _logger;

    public GetCourseHandler(ILogger<GetCourseHandler> logger, ICourseReadOnlyRepository courseReadOnlyRepository)
    {
        _courseReadOnlyRepository = courseReadOnlyRepository;
        _logger = logger;
    }

    public async Task<Domain.Course> Handle(GetCourseQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Get Course by Id: {Id}", request.Id);

        return await _courseReadOnlyRepository
            .GetFullCourse(request.Id)
            .ConfigureAwait(false);
    }
}