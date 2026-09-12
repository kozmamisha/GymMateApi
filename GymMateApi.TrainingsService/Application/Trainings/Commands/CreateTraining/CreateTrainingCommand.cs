using MediatR;

namespace GymMateApi.TrainingsService.Application.Trainings.Commands.CreateTraining;

public record CreateTrainingCommand(string Name, string Description) : IRequest;
