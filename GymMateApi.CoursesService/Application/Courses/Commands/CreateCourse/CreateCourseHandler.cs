using GymMateApi.CoursesService.Core;
using GymMateApi.CoursesService.Persistance.Interfaces;
using GymMateApi.Shared.Exceptions;
using MediatR;

namespace GymMateApi.CoursesService.Application.Courses.Commands.CreateCourse;

public class CreateCourseHandler(
    ICourseRepository courseRepository) : IRequestHandler<CreateCourseCommand>
{
    public async Task Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            throw new BadRequestException("Course name cannot be empty");

        var course = new CourseEntity { Name = request.Name };

        await courseRepository.CreateCourse(course, cancellationToken);
    }
}
