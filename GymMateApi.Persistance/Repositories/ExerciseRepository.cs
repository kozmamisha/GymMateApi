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
    public class ExerciseRepository : IExerciseRepository
    {
        private readonly GymMateDbContext _dbContext;
        public ExerciseRepository(GymMateDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateExercise(ExerciseEntity exercise)
        {
            await _dbContext.Exercises.AddAsync(exercise);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteExercise(ExerciseEntity exercise)
        {
            _dbContext.Exercises.Remove(exercise);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<ExerciseEntity>> GetAllExercises()
        {
            return await _dbContext.Exercises
                .AsNoTracking()
                .OrderBy(e => e.Id)
                .ToListAsync();
        }

        public async Task<ExerciseEntity?> GetExerciseById(Guid id)
        {
            return await _dbContext.Exercises
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task UpdateExercise(ExerciseEntity exercise)
        {
            _dbContext.Exercises.Update(exercise);
            await _dbContext.SaveChangesAsync();
        }
    }
}
