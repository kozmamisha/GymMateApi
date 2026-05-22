using MediatR;

namespace GymMateApi.Application.Trainings.Commands.CreateTraining;

public record CreateTrainingCommand(string Name, string Description) : IRequest;