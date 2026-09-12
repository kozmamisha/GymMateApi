using MediatR;

namespace GymMateApi.ExercisesService.Application.Exercises.Commands.DeleteExercise;

public record DeleteExerciseCommand(Guid Id) : IRequest;
