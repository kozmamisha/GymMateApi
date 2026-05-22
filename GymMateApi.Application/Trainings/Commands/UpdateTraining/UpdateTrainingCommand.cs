using MediatR;

namespace GymMateApi.Application.Trainings.Commands.UpdateTraining;

public record UpdateTrainingCommand(Guid Id, string Name, string Description) : IRequest;