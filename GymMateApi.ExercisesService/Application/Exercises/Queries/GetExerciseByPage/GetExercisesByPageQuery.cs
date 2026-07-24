using GymMateApi.Application.Dto;
using MediatR;

namespace GymMateApi.Application.Exercises.Queries.GetExerciseByPage;

public record GetExercisesByPageQuery(int Page, int PageSize) : IRequest<List<ExerciseDto>>;