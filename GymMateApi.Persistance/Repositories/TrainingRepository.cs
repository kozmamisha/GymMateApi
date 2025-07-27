using GymMateApi.Core.Entities;
using GymMateApi.Persistance;
using GymMateApi.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMateApi.Persistence.Repositories
{
    public class TrainingRepository : ITrainingRepository
    {
        private readonly GymMateDbContext _dbContext;
        public TrainingRepository(GymMateDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateTraining(TrainingEntity training)
        {
            await _dbContext.Trainings.AddAsync(training);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteTraining(TrainingEntity training)
        {
            _dbContext.Trainings.Remove(training);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<TrainingEntity>> GetAllTrainings()
        {
            return await _dbContext.Trainings
                .AsNoTracking()
                .Include(t => t.Exercises)
                .Include(t => t.Comments)
                .OrderBy(t => t.Id)
                .ToListAsync();
        }

        public async Task<TrainingEntity?> GetTrainingById(Guid id)
        {
            return await _dbContext.Trainings
                .AsNoTracking()
                .Include(t => t.Exercises)
                .Include(t => t.Comments)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task UpdateTraining(TrainingEntity training)
        {
            _dbContext.Trainings.Update(training);
            await _dbContext.SaveChangesAsync();
        }
    }
}
