using GymMateApi.ExercisesService.Application.Dto;
using MediatR;

namespace GymMateApi.ExercisesService.Application.Exercises.Queries.GetExerciseById;

public record GetExerciseByIdQuery(Guid Id) : IRequest<ExerciseDto>;
