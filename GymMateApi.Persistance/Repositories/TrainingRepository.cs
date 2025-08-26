using GymMateApi.Core.Entities;
using GymMateApi.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymMateApi.Persistence.Repositories
{
    public class TrainingRepository(GymMateDbContext dbContext) : ITrainingRepository
    {
        public async Task CreateTraining(TrainingEntity training, CancellationToken cancellationToken)
        {
            await dbContext.Trainings.AddAsync(training, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteTraining(TrainingEntity training, CancellationToken cancellationToken)
        {
            dbContext.Trainings.Remove(training);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<TrainingEntity>> GetAllTrainings(CancellationToken cancellationToken)
        {
            return await dbContext.Trainings
                .AsNoTracking()
                .Include(t => t.Exercises)
                .Include(t => t.Comments)
                .OrderBy(t => t.Id)
                .ToListAsync(cancellationToken);
        }

        public async Task<TrainingEntity?> GetTrainingById(Guid id, CancellationToken cancellationToken)
        {
            return await dbContext.Trainings
                .AsNoTracking()
                .Include(t => t.Exercises)
                .Include(t => t.Comments)
                .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
        }

        public async Task UpdateTraining(TrainingEntity training, CancellationToken cancellationToken)
        {
            dbContext.Trainings.Update(training);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
