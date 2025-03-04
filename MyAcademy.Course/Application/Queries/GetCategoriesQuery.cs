using MediatR;

namespace MyAcademy.Course.Application.Queries;

public record GetCategoriesQuery : IRequest<IEnumerable<Domain.Category>>;