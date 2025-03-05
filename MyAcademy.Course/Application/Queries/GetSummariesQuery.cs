using MediatR;
using MyAcademy.Course.Application.DTOs;

namespace MyAcademy.Course.Application.Queries;

public record GetSummariesQuery : IRequest<IEnumerable<Domain.Course>>;