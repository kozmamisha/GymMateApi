using MediatR;

namespace GymMateApi.ExercisesService.Application.Exercises.Commands.UpdateExercise;

public record UpdateExerciseCommand(Guid Id, string Name, string Description) : IRequest;
