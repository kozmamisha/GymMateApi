using GymMateApi.TrainingsService.Core;

namespace GymMateApi.TrainingsService.Persistance.Interfaces;

public interface ITrainingRepository
{
    Task<List<TrainingEntity>> GetAllTrainings(CancellationToken cancellationToken);
    Task<TrainingEntity?> GetTrainingById(Guid id, CancellationToken cancellationToken);
    Task CreateTraining(TrainingEntity training, CancellationToken cancellationToken);
    Task UpdateTraining(TrainingEntity training, CancellationToken cancellationToken);
    Task DeleteTraining(TrainingEntity training, CancellationToken cancellationToken);
}
