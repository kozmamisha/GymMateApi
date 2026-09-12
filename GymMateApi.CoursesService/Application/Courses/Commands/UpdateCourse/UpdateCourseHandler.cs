using GymMateApi.CoursesService.Persistance.Interfaces;
using GymMateApi.Shared.Exceptions;
using MediatR;

namespace GymMateApi.CoursesService.Application.Courses.Commands.UpdateCourse;

public class UpdateCourseHandler(
    ICourseRepository courseRepository) : IRequestHandler<UpdateCourseCommand>
{
    public async Task Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            throw new BadRequestException("Course name cannot be empty");

        var course = await courseRepository.GetCourseById(request.Id, cancellationToken)
                     ?? throw new EntityNotFoundException("Course not found");

        course.Name = request.Name;

        await courseRepository.UpdateCourse(course, cancellationToken);
    }
}
