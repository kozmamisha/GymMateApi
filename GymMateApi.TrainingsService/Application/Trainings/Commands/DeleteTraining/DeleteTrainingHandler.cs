using GymMateApi.Shared.Exceptions;
using GymMateApi.TrainingsService.Persistance.Interfaces;
using MediatR;

namespace GymMateApi.TrainingsService.Application.Trainings.Commands.DeleteTraining;

public class DeleteTrainingHandler(
    ITrainingRepository trainingRepository) : IRequestHandler<DeleteTrainingCommand>
{
    public async Task Handle(DeleteTrainingCommand request, CancellationToken cancellationToken)
    {
        var training = await trainingRepository.GetTrainingById(request.Id, cancellationToken)
                       ?? throw new EntityNotFoundException("Training not found");

        await trainingRepository.DeleteTraining(training, cancellationToken);
    }
}
