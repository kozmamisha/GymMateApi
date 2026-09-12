using GymMateApi.ExercisesService.Application.Dto;
using MediatR;

namespace GymMateApi.ExercisesService.Application.Exercises.Queries.GetExerciseByPage;

public record GetExercisesByPageQuery(int Page, int PageSize) : IRequest<List<ExerciseDto>>;
