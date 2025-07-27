using GymMateApi.Application.Dto;
using GymMateApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMateApi.Application.Interfaces
{
    public interface IExerciseService
    {
        Task<List<ExerciseDto>> GetAllAsync();
        Task<List<ExerciseDto>> GetByPage(int page, int pageSize);
        Task<ExerciseDto?> GetByIdAsync(Guid id);
        Task CreateAsync(string name, string description, Guid trainingId);
        Task UpdateAsync(Guid id, string name, string description);
        Task DeleteAsync(Guid id);
    }
}
