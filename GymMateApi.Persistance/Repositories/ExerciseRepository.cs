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
    public class ExerciseRepository(GymMateDbContext dbContext) : IExerciseRepository
    {
        public async Task CreateExercise(ExerciseEntity exercise, CancellationToken cancellationToken)
        {
            await dbContext.Exercises.AddAsync(exercise, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteExercise(ExerciseEntity exercise, CancellationToken cancellationToken)
        {
            dbContext.Exercises.Remove(exercise);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<ExerciseEntity>> GetAllExercises(CancellationToken cancellationToken)
        {
            return await dbContext.Exercises
                .AsNoTracking()
                .OrderBy(e => e.Id)
                .ToListAsync(cancellationToken);
        }

        public async Task<ExerciseEntity?> GetExerciseById(Guid id, CancellationToken cancellationToken)
        {
            return await dbContext.Exercises
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }
        
        public async Task<List<ExerciseEntity>> GetExercisesByPage(int page, int pageSize, CancellationToken cancellationToken)
        {
            return await dbContext.Exercises
                .AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task UpdateExercise(ExerciseEntity exercise, CancellationToken cancellationToken)
        {
            dbContext.Exercises.Update(exercise);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
