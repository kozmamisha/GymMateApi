using GymMateApi.CoursesService.Application.Dto;
using GymMateApi.CoursesService.Application.Extensions;
using GymMateApi.CoursesService.Persistance.Interfaces;
using GymMateApi.Shared.Exceptions;
using MediatR;

namespace GymMateApi.CoursesService.Application.Courses.Queries.GetCourseById;

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
