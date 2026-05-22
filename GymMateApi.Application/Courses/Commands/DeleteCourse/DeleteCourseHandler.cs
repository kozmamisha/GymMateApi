using GymMateApi.Application.Exceptions;
using GymMateApi.Persistence.Interfaces;
using MediatR;

namespace GymMateApi.Application.Courses.Commands.DeleteCourse;

public class DeleteCourseHandler(
    ICourseRepository courseRepository) : IRequestHandler<DeleteCourseCommand>
{
    public async Task Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await courseRepository.GetCourseById(request.Id, cancellationToken)
                     ?? throw new EntityNotFoundException("Course not found");

        await courseRepository.DeleteCourse(course, cancellationToken);
    }
}