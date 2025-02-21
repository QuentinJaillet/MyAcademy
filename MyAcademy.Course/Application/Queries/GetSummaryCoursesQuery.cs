using MediatR;
using MyAcademy.Course.Domain;

namespace MyAcademy.Course.Application.Queries;

public record GetSummaryCoursesQuery : IRequest<IReadOnlyList<SummaryCours>>;