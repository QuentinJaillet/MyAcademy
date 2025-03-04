using MyAcademy.Course.Models;

namespace MyAcademy.Course.Mappers;

public static class CategoryMapper
{
    public static Category ToModel(this Domain.Category domain)
    {
        return new Category(domain.Id, domain.Name);
    }

    public static IEnumerable<Category> ToModel(this IEnumerable<Domain.Category> domains)
    {
        return domains.Select(domain => domain.ToModel());
    }
}