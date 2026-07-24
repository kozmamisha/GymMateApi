using MediatR;

namespace GymMateApi.Application.Exercises.Commands.CreateExercise;

public record CreateExerciseCommand(string Name, string Description, Guid TrainingId) : IRequest;