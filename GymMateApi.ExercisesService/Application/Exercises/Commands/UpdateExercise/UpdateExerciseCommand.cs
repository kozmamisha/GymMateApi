using MediatR;

namespace GymMateApi.Application.Exercises.Commands.UpdateExercise;

public record UpdateExerciseCommand(Guid Id, string Name, string Description) : IRequest;