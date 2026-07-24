using GymMateApi.Application.Dto;
using GymMateApi.Application.Exceptions;
using GymMateApi.Application.Extensions;
using GymMateApi.Persistence.Interfaces;
using MediatR;

namespace GymMateApi.Application.Exercises.Queries.GetExerciseById;

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