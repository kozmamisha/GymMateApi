using GymMateApi.Application.Exceptions;
using GymMateApi.Persistence.Interfaces;
using MediatR;

namespace GymMateApi.Application.Courses.Commands.SubscribeToCourse;

public class SubscribeToCourseHandler(
    ICourseRepository courseRepository) : IRequestHandler<SubscribeToCourseCommand>
{
    public async Task Handle(SubscribeToCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await courseRepository.GetCourseById(request.CourseId, cancellationToken)
                     ?? throw new EntityNotFoundException("Course not found");

        var alreadySubscribed = course.Subscribers.Any(s => s.Id == request.UserId);
        if (alreadySubscribed)
            throw new BadRequestException("This user is already subscribed to this course");

        await courseRepository.SubscribeToCourse(request.CourseId, request.UserId, cancellationToken);
    }
}