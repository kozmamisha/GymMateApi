using GymMateApi.Application.Exceptions;
using GymMateApi.Persistence.Interfaces;
using MediatR;

namespace GymMateApi.Application.Courses.Commands.UnsubscribeFromCourse;

public class UnsubscribeFromCourseHandler(
    ICourseRepository courseRepository) : IRequestHandler<UnsubscribeFromCourseCommand>
{
    public async Task Handle(UnsubscribeFromCourseCommand request, CancellationToken cancellationToken)
    {
        _ = await courseRepository.GetCourseById(request.CourseId, cancellationToken)
            ?? throw new EntityNotFoundException("Course not found");

        await courseRepository.UnsubscribeFromCourse(request.CourseId, request.UserId, cancellationToken);
    }
}