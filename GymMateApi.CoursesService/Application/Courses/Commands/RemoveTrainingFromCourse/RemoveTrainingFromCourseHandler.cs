using GymMateApi.Application.Exceptions;
using GymMateApi.Persistence.Interfaces;
using MediatR;

namespace GymMateApi.Application.Courses.Commands.RemoveTrainingFromCourse;

public class RemoveTrainingFromCourseHandler(
    ICourseRepository courseRepository,
    ITrainingRepository trainingRepository) : IRequestHandler<RemoveTrainingFromCourseCommand>
{
    public async Task Handle(RemoveTrainingFromCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await courseRepository.GetCourseById(request.CourseId, cancellationToken)
                     ?? throw new EntityNotFoundException("Course not found");

        _ = await trainingRepository.GetTrainingById(request.TrainingId, cancellationToken)
            ?? throw new EntityNotFoundException("Training not found");

        var trainingExists = course.Trainings.Any(t => t.Id == request.TrainingId);
        if (!trainingExists)
            throw new EntityNotFoundException("There is no this training in this course");

        await courseRepository.RemoveTrainingFromCourse(request.CourseId, request.TrainingId, cancellationToken);
    }
}