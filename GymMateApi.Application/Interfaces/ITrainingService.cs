using GymMateApi.Application.Dto;
using GymMateApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMateApi.Application.Interfaces
{
    public interface ITrainingService
    {
        Task<List<TrainingDto>> GetAllAsync();
        Task<TrainingDto?> GetByIdAsync(Guid id);
        Task CreateAsync(string name, string description);
        Task UpdateAsync(Guid id, string name, string description);
        Task DeleteAsync(Guid id);
    }
}
