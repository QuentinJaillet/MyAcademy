using Microsoft.EntityFrameworkCore;
using MyAcademy.Course.Domain;
using MyAcademy.Course.Infrastructure.Persistence;

namespace MyAcademy.Course.Infrastructure.Repositories;

public class CategoryReadOnlyRepository : ICategoryReadOnlyRepository
{
    private readonly ApplicationDbContext _context;

    public CategoryReadOnlyRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IEnumerable<Category>> GetCategoriesAsync(CancellationToken cancellationToken)
    {
        return await _context.Categories
            .OrderBy(c => c.Name)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }
}