using GymMateApi.Application.Dto;
using GymMateApi.Application.Extensions;
using GymMateApi.Persistence.Interfaces;
using MediatR;

namespace GymMateApi.Application.Exercises.Queries.GetAllExercises;

public class GetAllExercisesHandler(
    IExerciseRepository exerciseRepository) : IRequestHandler<GetAllExercisesQuery, List<ExerciseDto>>
{
    public async Task<List<ExerciseDto>> Handle(GetAllExercisesQuery request, CancellationToken cancellationToken)
    {
        var exercises = await exerciseRepository.GetAllExercises(cancellationToken);
        return exercises.ToDtoList();
    }
}