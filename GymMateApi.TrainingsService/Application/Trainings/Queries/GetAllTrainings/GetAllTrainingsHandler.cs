using GymMateApi.Application.Dto;
using GymMateApi.Application.Extensions;
using GymMateApi.Persistence.Interfaces;
using MediatR;

namespace GymMateApi.Application.Trainings.Queries.GetAllTrainings;

public class GetAllTrainingsHandler(
    ITrainingRepository trainingRepository) : IRequestHandler<GetAllTrainingsQuery, List<TrainingDto>>
{
    public async Task<List<TrainingDto>> Handle(GetAllTrainingsQuery request, CancellationToken cancellationToken)
    {
        var trainings = await trainingRepository.GetAllTrainings(cancellationToken);
        return trainings.ToDtoList();
    }
}