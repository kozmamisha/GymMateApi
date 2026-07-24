using GymMateApi.Application.Dto;
using GymMateApi.Application.Exceptions;
using GymMateApi.Application.Extensions;
using GymMateApi.Persistence.Interfaces;
using MediatR;

namespace GymMateApi.Application.Trainings.Queries.GetTrainingById;

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