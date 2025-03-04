using MediatR;
using MyAcademy.Course.Application.Queries;
using MyAcademy.Course.Infrastructure.Repositories;

namespace MyAcademy.Course.Application.Handlers;

public class GetCategoriesHanlder : IRequestHandler<GetCategoriesQuery, IEnumerable<Domain.Category>>
{
    private readonly ILogger<GetCategoriesHanlder> _logger;
    private readonly ICategoryReadOnlyRepository _categoryReadOnlyRepository;

    public GetCategoriesHanlder(ILogger<GetCategoriesHanlder> logger,
        ICategoryReadOnlyRepository categoryReadOnlyRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _categoryReadOnlyRepository = categoryReadOnlyRepository ??
                                      throw new ArgumentNullException(nameof(categoryReadOnlyRepository));
    }

    public async Task<IEnumerable<Domain.Category>> Handle(GetCategoriesQuery request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting categories");

        return await _categoryReadOnlyRepository
            .GetCategoriesAsync(cancellationToken)
            .ConfigureAwait(false);
    }
}