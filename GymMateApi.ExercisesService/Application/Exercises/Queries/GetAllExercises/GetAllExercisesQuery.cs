using GymMateApi.Application.Dto;
using MediatR;

namespace GymMateApi.Application.Exercises.Queries.GetAllExercises;

public record GetAllExercisesQuery() : IRequest<List<ExerciseDto>>;