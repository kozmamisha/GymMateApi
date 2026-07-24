using GymMateApi.Application.Exceptions;
using GymMateApi.Persistence.Interfaces;
using MediatR;

namespace GymMateApi.Application.Courses.Commands.AddTrainingToCourse;

public class AddTrainingToCourseHandler(
    ICourseRepository courseRepository,
    ITrainingRepository trainingRepository) : IRequestHandler<AddTrainingToCourseCommand>
{
    public async Task Handle(AddTrainingToCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await courseRepository.GetCourseById(request.CourseId, cancellationToken)
                     ?? throw new EntityNotFoundException("Course not found");

        _ = await trainingRepository.GetTrainingById(request.TrainingId, cancellationToken)
            ?? throw new EntityNotFoundException("Training not found");

        var alreadyAdded = course.Trainings.Any(t => t.Id == request.TrainingId);
        if (alreadyAdded)
            throw new BadRequestException("This training is already added to this course");

        await courseRepository.AddTrainingToCourse(request.CourseId, request.TrainingId, cancellationToken);
    }
}