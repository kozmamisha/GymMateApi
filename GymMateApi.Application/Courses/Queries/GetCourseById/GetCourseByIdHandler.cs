using GymMateApi.Application.Dto;
using GymMateApi.Application.Exceptions;
using GymMateApi.Application.Extensions;
using GymMateApi.Persistence.Interfaces;
using MediatR;

namespace GymMateApi.Application.Courses.Queries.GetCourseById;

public class GetCourseByIdHandler(
    ICourseRepository courseRepository) : IRequestHandler<GetCourseByIdQuery, CourseDto>
{
    public async Task<CourseDto> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
    {
        var course = await courseRepository.GetCourseById(request.Id, cancellationToken)
                     ?? throw new EntityNotFoundException("Course not found");

        return course.ToDto();
    }
}