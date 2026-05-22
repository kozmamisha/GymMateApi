using GymMateApi.Application.Dto;
using GymMateApi.Application.Extensions;
using GymMateApi.Persistence.Interfaces;
using MediatR;

namespace GymMateApi.Application.Courses.Queries.GetAllCourses;

public class GetAllCoursesHandler(
    ICourseRepository courseRepository) : IRequestHandler<GetAllCoursesQuery, List<CourseDto>>
{
    public async Task<List<CourseDto>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
    {
        var courses = await courseRepository.GetAllCourses(cancellationToken);
        return courses.ToDtoList();
    }
}