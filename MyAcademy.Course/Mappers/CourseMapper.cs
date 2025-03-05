namespace MyAcademy.Course.Mappers;

public static class CourseMapper
{
    public static Models.Course ToModel(this Domain.Course course)
    {
        return new Models.Course
        {
            Id = course.Id,
            Name = course.Title,
            Description = course.Description
        };
    }

    public static IEnumerable<Models.Course> ToModel(this IEnumerable<Domain.Course> courses)
    {
        return courses.Select(course => course.ToModel());
    }
}