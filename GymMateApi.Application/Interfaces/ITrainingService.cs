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
        Task<List<TrainingDto>> GetAllAsync(CancellationToken cancellationToken);
        Task<TrainingDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task CreateAsync(string name, string description, CancellationToken cancellationToken);
        Task UpdateAsync(Guid id, string name, string description, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
