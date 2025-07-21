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
        Task<List<TrainingEntity>> GetAllTrainings();
        Task<TrainingEntity?> GetTrainingById(Guid id);
        Task CreateTraining(TrainingEntity training);
        Task UpdateTraining(TrainingEntity training);
        Task DeleteTraining(TrainingEntity training);
    }
}
