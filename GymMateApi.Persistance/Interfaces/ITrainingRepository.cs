using GymMateApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMateApi.Persistence.Interfaces
{
    public interface ITrainingRepository
    {
        Task<List<TrainingEntity>> GetAllTrainings(CancellationToken cancellationToken);
        Task<TrainingEntity?> GetTrainingById(Guid id, CancellationToken cancellationToken);
        Task CreateTraining(TrainingEntity training, CancellationToken cancellationToken);
        Task UpdateTraining(TrainingEntity training, CancellationToken cancellationToken);
        Task DeleteTraining(TrainingEntity training, CancellationToken cancellationToken);
    }
}
