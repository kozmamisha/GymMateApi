using MediatR;

namespace GymMateApi.TrainingsService.Application.Trainings.Commands.UpdateTraining;

public record UpdateTrainingCommand(Guid Id, string Name, string Description) : IRequest;
