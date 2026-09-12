using GymMateApi.ExercisesService.Application.Dto;
using GymMateApi.ExercisesService.Application.Extensions;
using GymMateApi.ExercisesService.Persistance.Interfaces;
using MediatR;

namespace GymMateApi.ExercisesService.Application.Exercises.Queries.GetExerciseByPage;

public class GetExercisesByPageHandler(
    IExerciseRepository exerciseRepository) : IRequestHandler<GetExercisesByPageQuery, List<ExerciseDto>>
{
    public async Task<List<ExerciseDto>> Handle(GetExercisesByPageQuery request, CancellationToken cancellationToken)
    {
        var exercises = await exerciseRepository.GetExercisesByPage(request.Page, request.PageSize, cancellationToken);
        return exercises.ToDtoList();
    }
}
