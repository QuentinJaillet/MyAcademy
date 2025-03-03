using MyAcademy.Course.Application.Commands;
using MyAcademy.Course.Models;

namespace MyAcademy.Course.Mappers;

public static class CreateCourseMapper
{
    public static CreateCourseCommand ToCommand(this CreateCourseRequest request)
    {
        return new CreateCourseCommand
        {
            Title = request.Title,
            Subtitle = request.Subtitle,
            Description = request.Description,
            ImageUrl = request.ImageUrl,
            CategoryId = request.CategoryId
        };
    }
}