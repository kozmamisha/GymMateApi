using GymMateApi.CoursesService.Persistance.Interfaces;
using GymMateApi.Shared.Exceptions;
using MediatR;

namespace GymMateApi.CoursesService.Application.Courses.Commands.RemoveTrainingFromCourse;

public class RemoveTrainingFromCourseHandler(
    ICourseRepository courseRepository) : IRequestHandler<RemoveTrainingFromCourseCommand>
{
    public async Task Handle(RemoveTrainingFromCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await courseRepository.GetCourseById(request.CourseId, cancellationToken)
                     ?? throw new EntityNotFoundException("Course not found");

        var trainingExists = course.TrainingIds.Any(t => t == request.TrainingId);
        if (!trainingExists)
            throw new EntityNotFoundException("There is no this training in this course");

        await courseRepository.RemoveTrainingFromCourse(request.CourseId, request.TrainingId, cancellationToken);
    }
}
