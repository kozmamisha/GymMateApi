using GymMateApi.Application.Dto;
using MediatR;

namespace GymMateApi.Application.Exercises.Queries.GetExerciseById;

public record GetExerciseByIdQuery(Guid Id) : IRequest<ExerciseDto>;