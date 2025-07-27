using GymMateApi.Application.Dto;
using GymMateApi.Core.Entities;

namespace GymMateApi.Application.Extensions;

public static class CourseMappingExtension
{
    public static CourseDto ToDto(this CourseEntity course)
    {
        return new CourseDto
        {
            Id = course.Id,
            Name = course.Name,
            Trainings = course.Trainings.Select(e => new CourseTrainingDto()
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description
            }).ToList(),
        };
    }

    public static List<CourseDto> ToDtoList(this IEnumerable<CourseEntity> courses)
    {
        return courses.Select(t => t.ToDto()).ToList();
    }
}