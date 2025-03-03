using MediatR;

namespace MyAcademy.Course.Application.Commands;

public record CreateCourseCommand : IRequest<Guid>
{
    public string Title { get; init; }
    public string Subtitle { get; init; }
    public string Description { get; init; }
    public string ImageUrl { get; init; }
    public Guid CategoryId { get; init; }
}