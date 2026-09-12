using GymMateApi.CoursesService.Application.Dto;
using GymMateApi.CoursesService.Core;

namespace GymMateApi.CoursesService.Application.Extensions;

public static class CourseMappingExtension
{
    public static CourseDto ToDto(this CourseEntity course)
    {
        return new CourseDto
        {
            Id = course.Id,
            Name = course.Name,
            AverageRating = course.AverageRating,
            SubscriberIds = course.SubscriberIds.ToList(),
            TrainingIds = course.TrainingIds.ToList()
        };
    }

    public static List<CourseDto> ToDtoList(this IEnumerable<CourseEntity> courses)
    {
        return courses.Select(c => c.ToDto()).ToList();
    }
}
