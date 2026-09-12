using GymMateApi.ExercisesService.Persistance.Interfaces;
using GymMateApi.Shared.Exceptions;
using MediatR;

namespace GymMateApi.ExercisesService.Application.Exercises.Commands.UpdateExercise;

public class UpdateExerciseHandler(
    IExerciseRepository exerciseRepository) : IRequestHandler<UpdateExerciseCommand>
{
    public async Task Handle(UpdateExerciseCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            throw new BadRequestException("Exercise name cannot be empty");

        if (string.IsNullOrWhiteSpace(request.Description))
            throw new BadRequestException("Exercise description cannot be empty");

        var exercise = await exerciseRepository.GetExerciseById(request.Id, cancellationToken)
                       ?? throw new EntityNotFoundException("This exercise not found");

        exercise.Name = request.Name;
        exercise.Description = request.Description;

        await exerciseRepository.UpdateExercise(exercise, cancellationToken);
    }
}
