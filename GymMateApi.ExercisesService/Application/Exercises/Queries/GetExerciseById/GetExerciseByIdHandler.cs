using GymMateApi.ExercisesService.Application.Dto;
using GymMateApi.ExercisesService.Application.Extensions;
using GymMateApi.ExercisesService.Persistance.Interfaces;
using GymMateApi.Shared.Exceptions;
using MediatR;

namespace GymMateApi.ExercisesService.Application.Exercises.Queries.GetExerciseById;

public class GetExerciseByIdHandler(
    IExerciseRepository exerciseRepository) : IRequestHandler<GetExerciseByIdQuery, ExerciseDto>
{
    public async Task<ExerciseDto> Handle(GetExerciseByIdQuery request, CancellationToken cancellationToken)
    {
        var exercise = await exerciseRepository.GetExerciseById(request.Id, cancellationToken)
                       ?? throw new EntityNotFoundException("This exercise not found");

        return exercise.ToDto();
    }
}
