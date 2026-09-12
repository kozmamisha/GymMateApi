using GymMateApi.ExercisesService.Persistance.Interfaces;
using GymMateApi.Shared.Exceptions;
using MediatR;

namespace GymMateApi.ExercisesService.Application.Exercises.Commands.DeleteExercise;

public class DeleteExerciseHandler(
    IExerciseRepository exerciseRepository) : IRequestHandler<DeleteExerciseCommand>
{
    public async Task Handle(DeleteExerciseCommand request, CancellationToken cancellationToken)
    {
        var exercise = await exerciseRepository.GetExerciseById(request.Id, cancellationToken)
                       ?? throw new EntityNotFoundException("This exercise not found");

        await exerciseRepository.DeleteExercise(exercise, cancellationToken);
    }
}
