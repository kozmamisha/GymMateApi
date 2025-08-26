using GymMateApi.Core.Entities;

namespace GymMateApi.Persistence.Interfaces
{
    public interface IExerciseRepository
    {
        Task<List<ExerciseEntity>> GetAllExercises(CancellationToken cancellationToken);
        Task<ExerciseEntity?> GetExerciseById(Guid id, CancellationToken cancellationToken);
        Task<List<ExerciseEntity>> GetExercisesByPage(int page, int pageSize, CancellationToken cancellationToken);
        Task CreateExercise(ExerciseEntity exercise, CancellationToken cancellationToken);
        Task UpdateExercise(ExerciseEntity exercise, CancellationToken cancellationToken);
        Task DeleteExercise(ExerciseEntity exercise, CancellationToken cancellationToken);
    }
}
