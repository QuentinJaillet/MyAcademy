namespace MyAcademy.Course.Infrastructure.Repositories;

public interface ICategoryReadOnlyRepository
{
    Task<IEnumerable<Domain.Category>> GetCategoriesAsync(CancellationToken cancellationToken);
}