using GymMateApi.Application.Dto;

namespace GymMateApi.Application.Interfaces
{
    public interface IExerciseService
    {
        Task<List<ExerciseDto>> GetAllAsync(CancellationToken cancellationToken);
        Task<List<ExerciseDto>> GetByPage(int page, int pageSize, CancellationToken cancellationToken);
        Task<ExerciseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task CreateAsync(string name, string description, Guid trainingId, CancellationToken cancellationToken);
        Task UpdateAsync(Guid id, string name, string description, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
