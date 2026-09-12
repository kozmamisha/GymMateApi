using GymMateApi.ExercisesService.Application.Dto;
using GymMateApi.ExercisesService.Application.Extensions;
using GymMateApi.ExercisesService.Persistance.Interfaces;
using MediatR;

namespace GymMateApi.ExercisesService.Application.Exercises.Queries.GetAllExercises;

public class GetAllExercisesHandler(
    IExerciseRepository exerciseRepository) : IRequestHandler<GetAllExercisesQuery, List<ExerciseDto>>
{
    public async Task<List<ExerciseDto>> Handle(GetAllExercisesQuery request, CancellationToken cancellationToken)
    {
        var exercises = await exerciseRepository.GetAllExercises(cancellationToken);
        return exercises.ToDtoList();
    }
}
