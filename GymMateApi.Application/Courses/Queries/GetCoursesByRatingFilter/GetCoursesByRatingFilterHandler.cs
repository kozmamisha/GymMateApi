using GymMateApi.Application.Dto;
using GymMateApi.Application.Exceptions;
using GymMateApi.Application.Extensions;
using GymMateApi.Persistence.Interfaces;
using MediatR;

namespace GymMateApi.Application.Courses.Queries.GetCoursesByRatingFilter;

public class GetCoursesByRatingFilterHandler(
    ICourseRepository courseRepository) : IRequestHandler<GetCoursesByRatingFilterQuery, List<CourseDto>>
{
    public async Task<List<CourseDto>> Handle(GetCoursesByRatingFilterQuery request, CancellationToken cancellationToken)
    {
        if (request.Rating < 1 || request.Rating > 5)
            throw new BadRequestException("Rating must be between 1 and 5.");

        var courses = await courseRepository.GetCoursesByRatingFilter(request.Rating, cancellationToken);
        return courses.ToDtoList();
    }
}