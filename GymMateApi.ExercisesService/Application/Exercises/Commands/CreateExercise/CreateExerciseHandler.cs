using GymMateApi.ExercisesService.Core;
using GymMateApi.ExercisesService.Persistance.Interfaces;
using GymMateApi.Shared.Exceptions;
using MediatR;

namespace GymMateApi.ExercisesService.Application.Exercises.Commands.CreateExercise;

public class CreateExerciseHandler(
    IExerciseRepository exerciseRepository) : IRequestHandler<CreateExerciseCommand>
{
    public async Task Handle(CreateExerciseCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            throw new BadRequestException("Exercise name cannot be empty");

        if (string.IsNullOrWhiteSpace(request.Description))
            throw new BadRequestException("Exercise description cannot be empty");

        if (request.TrainingId == Guid.Empty)
            throw new BadRequestException("Training id cannot be empty");

        var exercise = new ExerciseEntity
        {
            Name = request.Name,
            Description = request.Description,
            TrainingId = request.TrainingId
        };

        await exerciseRepository.CreateExercise(exercise, cancellationToken);
    }
}
