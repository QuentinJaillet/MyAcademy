using MediatR;

namespace MyAcademy.Course.Application.Queries;

public record GetCourseQuery(Guid Id) : IRequest<Domain.Course>;