using GymMateApi.Application.Dto;
using GymMateApi.Application.Extensions;
using GymMateApi.Persistence.Interfaces;
using MediatR;

namespace GymMateApi.Application.Exercises.Queries.GetExerciseByPage;

public class GetExercisesByPageHandler(
    IExerciseRepository exerciseRepository) : IRequestHandler<GetExercisesByPageQuery, List<ExerciseDto>>
{
    public async Task<List<ExerciseDto>> Handle(GetExercisesByPageQuery request, CancellationToken cancellationToken)
    {
        var exercises = await exerciseRepository.GetExercisesByPage(request.Page, request.PageSize, cancellationToken);
        return exercises.ToDtoList();
    }
}