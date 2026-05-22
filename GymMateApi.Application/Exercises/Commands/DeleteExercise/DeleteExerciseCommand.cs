using MediatR;

namespace GymMateApi.Application.Exercises.Commands.DeleteExercise;

public record DeleteExerciseCommand(Guid Id) : IRequest;