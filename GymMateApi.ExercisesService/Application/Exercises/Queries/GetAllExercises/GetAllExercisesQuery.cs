using GymMateApi.ExercisesService.Application.Dto;
using MediatR;

namespace GymMateApi.ExercisesService.Application.Exercises.Queries.GetAllExercises;

public record GetAllExercisesQuery() : IRequest<List<ExerciseDto>>;
