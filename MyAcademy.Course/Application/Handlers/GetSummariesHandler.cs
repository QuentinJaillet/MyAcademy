using MediatR;
using MyAcademy.Course.Application.DTOs;
using MyAcademy.Course.Application.Queries;
using MyAcademy.Course.Infrastructure.Repositories;

namespace MyAcademy.Course.Application.Handlers;

public class GetSummariesHandler : IRequestHandler<GetSummariesQuery, IEnumerable<Domain.Course>>
{
    private readonly ILogger<GetSummariesHandler> _logger;
    private readonly ICourseReadOnlyRepository _courseReadOnlyRepository;

    public GetSummariesHandler(ILogger<GetSummariesHandler> logger, ICourseReadOnlyRepository courseReadOnlyRepository)
    {
        _logger = logger;
        _courseReadOnlyRepository = courseReadOnlyRepository;
    }

    public async Task<IEnumerable<Domain.Course>> Handle(GetSummariesQuery request,
        CancellationToken cancellationToken)
    {
        return await _courseReadOnlyRepository
            .GetFullCourses()
            .ConfigureAwait(false);
    }
}