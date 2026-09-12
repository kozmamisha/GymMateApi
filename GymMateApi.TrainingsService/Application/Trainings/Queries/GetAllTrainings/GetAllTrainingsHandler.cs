using GymMateApi.TrainingsService.Application.Dto;
using GymMateApi.TrainingsService.Application.Extensions;
using GymMateApi.TrainingsService.Persistance.Interfaces;
using MediatR;

namespace GymMateApi.TrainingsService.Application.Trainings.Queries.GetAllTrainings;

public class GetAllTrainingsHandler(
    ITrainingRepository trainingRepository) : IRequestHandler<GetAllTrainingsQuery, List<TrainingDto>>
{
    public async Task<List<TrainingDto>> Handle(GetAllTrainingsQuery request, CancellationToken cancellationToken)
    {
        var trainings = await trainingRepository.GetAllTrainings(cancellationToken);
        return trainings.ToDtoList();
    }
}
