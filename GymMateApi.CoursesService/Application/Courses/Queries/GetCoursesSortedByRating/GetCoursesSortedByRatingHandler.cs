using GymMateApi.CoursesService.Application.Dto;
using GymMateApi.CoursesService.Application.Extensions;
using GymMateApi.CoursesService.Persistance.Interfaces;
using MediatR;

namespace GymMateApi.CoursesService.Application.Courses.Queries.GetCoursesSortedByRating;

public class GetCoursesSortedByRatingHandler(
    ICourseRepository courseRepository) : IRequestHandler<GetCoursesSortedByRatingQuery, List<CourseDto>>
{
    public async Task<List<CourseDto>> Handle(GetCoursesSortedByRatingQuery request,
        CancellationToken cancellationToken)
    {
        var courses = await courseRepository.GetCoursesSortedByRating(request.IsDescending, cancellationToken);
        return courses.ToDtoList();
    }
}
