using MediatR;

namespace GymMateApi.ExercisesService.Application.Exercises.Commands.CreateExercise;

public record CreateExerciseCommand(string Name, string Description, Guid TrainingId) : IRequest;
