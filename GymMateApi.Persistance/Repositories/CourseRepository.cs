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
                .OrderBy(c => c.Id)
                .ToListAsync();
        }

        public async Task<CourseEntity?> GetCourseById(Guid id)
        {
            return await _dbContext.Courses
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task UpdateCourse(CourseEntity course)
        {
            _dbContext.Courses.Update(course);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RateCourseAsync(Guid courseId, int rating)
        {
            var course = await GetCourseById(courseId);
            
            course.Rating.Add(rating);
            
            await _dbContext.SaveChangesAsync();
        }

        public Task AddTrainingToCourse(Guid courseId, Guid trainingId)
        {
            throw new NotImplementedException();
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
