using GymMateApi.Application.Exceptions;
using GymMateApi.Core.Entities;
using GymMateApi.Persistence.Interfaces;
using MediatR;

namespace GymMateApi.Application.Courses.Commands.CreateCourse;

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