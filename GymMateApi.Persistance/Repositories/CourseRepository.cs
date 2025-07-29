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
    public class CourseRepository : ICourseRepository
    {
        private readonly GymMateDbContext _dbContext;
        public CourseRepository(GymMateDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateCourse(CourseEntity course)
        {
            await _dbContext.Courses.AddAsync(course);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteCourse(CourseEntity course)
        {
            _dbContext.Courses.Remove(course);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<CourseEntity>> GetAllCourses()
        {
            return await _dbContext.Courses
                .AsNoTracking()
                .Include(t => t.Trainings)
                .OrderBy(c => c.Id)
                .ToListAsync();
        }

        public async Task<CourseEntity?> GetCourseById(Guid id)
        {
            return await _dbContext.Courses
                .AsNoTracking()
                .Include(t => t.Trainings)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<CourseEntity>> GetCoursesByRatingFilter(int rating)
        {
            var courses = await _dbContext.Courses
                .AsNoTracking()
                .Include(t => t.Trainings)
                .ToListAsync();

            var filteredCourses = courses.Where(c => c.AverageRating > rating);
            
            return filteredCourses.ToList();
        }

        public async Task UpdateCourse(CourseEntity course)
        {
            _dbContext.Courses.Update(course);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RateCourse(Guid courseId, int rating)
        {
            var course = await _dbContext.Courses.FindAsync(courseId);
            
            course.Ratings.Add(rating);
            
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddTrainingToCourse(Guid courseId, Guid trainingId)
        {
            var course = await _dbContext.Courses.FindAsync(courseId);
            var training = await _dbContext.Trainings.FindAsync(trainingId);
            
            course.Trainings.Add(training);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveTrainingFromCourse(Guid courseId, Guid trainingId)
        {
            var course = await _dbContext.Courses
                .Include(c => c.Trainings)
                .FirstOrDefaultAsync(c => c.Id == courseId);
            
            var training = await _dbContext.Trainings.FindAsync(trainingId);
            
            course.Trainings.Remove(training);
            await _dbContext.SaveChangesAsync();
        }

        public Task SubscribeAsync(Guid courseId, Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task UnsubscribeAsync(Guid courseId, Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
