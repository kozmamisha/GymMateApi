using GymMateApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMateApi.Persistence.Interfaces
{
    public interface IExerciseRepository
    {
        Task<List<ExerciseEntity>> GetAllExercises();
        Task<ExerciseEntity?> GetExerciseById(Guid id);
        Task CreateExercise(ExerciseEntity exercise);
        Task UpdateExercise(ExerciseEntity exercise);
        Task DeleteExercise(ExerciseEntity exercise);
    }
}
