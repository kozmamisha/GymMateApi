using GymMateApi.Shared.Exceptions;
using GymMateApi.TrainingsService.Persistance.Interfaces;
using MediatR;

namespace GymMateApi.TrainingsService.Application.Trainings.Commands.UpdateTraining;

public class UpdateTrainingHandler(
    ITrainingRepository trainingRepository) : IRequestHandler<UpdateTrainingCommand>
{
    public async Task Handle(UpdateTrainingCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            throw new BadRequestException("Training name cannot be empty");

        if (string.IsNullOrWhiteSpace(request.Description))
            throw new BadRequestException("Training description cannot be empty");

        var training = await trainingRepository.GetTrainingById(request.Id, cancellationToken)
                       ?? throw new EntityNotFoundException("Training not found");

        training.Name = request.Name;
        training.Description = request.Description;

        await trainingRepository.UpdateTraining(training, cancellationToken);
    }
}
