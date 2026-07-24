using MediatR;

namespace GymMateApi.Application.Trainings.Commands.DeleteTraining;

public record DeleteTrainingCommand(Guid Id) : IRequest;