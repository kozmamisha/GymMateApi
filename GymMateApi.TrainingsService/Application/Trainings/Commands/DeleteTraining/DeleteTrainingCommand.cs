using MediatR;

namespace GymMateApi.TrainingsService.Application.Trainings.Commands.DeleteTraining;

public record DeleteTrainingCommand(Guid Id) : IRequest;
