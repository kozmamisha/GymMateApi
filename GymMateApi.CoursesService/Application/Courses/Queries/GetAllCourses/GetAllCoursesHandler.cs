using GymMateApi.CoursesService.Application.Dto;
using GymMateApi.CoursesService.Application.Extensions;
using GymMateApi.CoursesService.Persistance.Interfaces;
using MediatR;

namespace GymMateApi.CoursesService.Application.Courses.Queries.GetAllCourses;

public class GetAllCoursesHandler(
    ICourseRepository courseRepository) : IRequestHandler<GetAllCoursesQuery, List<CourseDto>>
{
    public async Task<List<CourseDto>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
    {
        var courses = await courseRepository.GetAllCourses(cancellationToken);
        return courses.ToDtoList();
    }
}
