using GymMateApi.Shared.Exceptions;
using GymMateApi.TrainingsService.Core;
using GymMateApi.TrainingsService.Persistance.Interfaces;
using MediatR;

namespace GymMateApi.TrainingsService.Application.Trainings.Commands.CreateTraining;

public class CreateTrainingHandler(
    ITrainingRepository trainingRepository) : IRequestHandler<CreateTrainingCommand>
{
    public async Task Handle(CreateTrainingCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            throw new BadRequestException("Training name cannot be empty");

        if (string.IsNullOrWhiteSpace(request.Description))
            throw new BadRequestException("Training description cannot be empty");

        var training = new TrainingEntity
        {
            Name = request.Name,
            Description = request.Description
        };

        await trainingRepository.CreateTraining(training, cancellationToken);
    }
}
