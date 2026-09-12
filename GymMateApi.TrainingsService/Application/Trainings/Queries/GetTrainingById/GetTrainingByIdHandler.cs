using GymMateApi.Shared.Exceptions;
using GymMateApi.TrainingsService.Application.Dto;
using GymMateApi.TrainingsService.Application.Extensions;
using GymMateApi.TrainingsService.Persistance.Interfaces;
using MediatR;

namespace GymMateApi.TrainingsService.Application.Trainings.Queries.GetTrainingById;

public class GetTrainingByIdHandler(
    ITrainingRepository trainingRepository) : IRequestHandler<GetTrainingByIdQuery, TrainingDto>
{
    public async Task<TrainingDto> Handle(GetTrainingByIdQuery request, CancellationToken cancellationToken)
    {
        var training = await trainingRepository.GetTrainingById(request.Id, cancellationToken)
                       ?? throw new EntityNotFoundException("Training not found");

        return training.ToDto();
    }
}
