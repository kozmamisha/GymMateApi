using GymMateApi.CoursesService.Persistance.Interfaces;
using GymMateApi.Shared.Exceptions;
using MediatR;

namespace GymMateApi.CoursesService.Application.Courses.Commands.RateCourse;

public class RateCourseHandler(
    ICourseRepository courseRepository) : IRequestHandler<RateCourseCommand>
{
    public async Task Handle(RateCourseCommand request, CancellationToken cancellationToken)
    {
        _ = await courseRepository.GetCourseById(request.CourseId, cancellationToken)
            ?? throw new EntityNotFoundException("Course not found");

        if (request.Rating is < 1 or > 5)
            throw new BadRequestException("Rating must be between 1 and 5.");

        await courseRepository.RateCourse(request.CourseId, request.Rating, cancellationToken);
    }
}
