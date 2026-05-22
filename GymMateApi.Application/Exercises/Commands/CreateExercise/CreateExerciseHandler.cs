using GymMateApi.Application.Exceptions;
using GymMateApi.Core.Entities;
using GymMateApi.Persistence.Interfaces;
using MediatR;

namespace GymMateApi.Application.Exercises.Commands.CreateExercise;

public class CreateExerciseHandler(
    IExerciseRepository exerciseRepository,
    ITrainingRepository trainingRepository) : IRequestHandler<CreateExerciseCommand>
{
    public async Task Handle(CreateExerciseCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            throw new BadRequestException("Exercise name cannot be empty");

        if (string.IsNullOrWhiteSpace(request.Description))
            throw new BadRequestException("Exercise description cannot be empty");

        _ = await trainingRepository.GetTrainingById(request.TrainingId, cancellationToken)
            ?? throw new EntityNotFoundException("Training not found");

        var exercise = new ExerciseEntity
        {
            Name = request.Name,
            Description = request.Description,
            TrainingId = request.TrainingId
        };

        await exerciseRepository.CreateExercise(exercise, cancellationToken);
    }
}