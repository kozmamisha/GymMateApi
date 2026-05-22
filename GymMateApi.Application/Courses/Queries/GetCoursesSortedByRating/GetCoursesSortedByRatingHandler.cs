using GymMateApi.Application.Dto;
using GymMateApi.Application.Extensions;
using GymMateApi.Persistence.Interfaces;
using MediatR;

namespace GymMateApi.Application.Courses.Queries.GetCoursesSortedByRating;

public class GetCoursesSortedByRatingHandler(
    ICourseRepository courseRepository) : IRequestHandler<GetCoursesSortedByRatingQuery, List<CourseDto>>
{
    public async Task<List<CourseDto>> Handle(GetCoursesSortedByRatingQuery request, CancellationToken cancellationToken)
    {
        var courses = await courseRepository.GetCoursesSortedByRating(request.IsDescending, cancellationToken);
        return courses.ToDtoList();
    }
}