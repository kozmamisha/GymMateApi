using GymMateApi.Core.Entities;
using GymMateApi.Persistance;
using GymMateApi.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymMateApi.Persistence.Repositories
{
    public class CourseRepository(GymMateDbContext dbContext) : ICourseRepository
    {
        public async Task CreateCourse(CourseEntity course, CancellationToken cancellationToken)
        {
            await dbContext.Courses.AddAsync(course, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteCourse(CourseEntity course, CancellationToken cancellationToken)
        {
            dbContext.Courses.Remove(course);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<CourseEntity>> GetAllCourses(CancellationToken cancellationToken)
        {
            return await dbContext.Courses
                .AsNoTracking()
                .Include(t => t.Trainings)
                .Include(s => s.Subscribers)
                .OrderBy(c => c.Id)
                .ToListAsync(cancellationToken);
        }

        public async Task<CourseEntity?> GetCourseById(Guid id, CancellationToken cancellationToken)
        {
            return await dbContext.Courses
                .AsNoTracking()
                .Include(t => t.Trainings)
                .Include(s => s.Subscribers)
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async Task<List<CourseEntity>> GetCoursesByRatingFilter(int rating, CancellationToken cancellationToken)
        {
            var courses = await dbContext.Courses
                .AsNoTracking()
                .Include(t => t.Trainings)
                .Include(s => s.Subscribers)
                .ToListAsync(cancellationToken);

            var filteredCourses = courses.Where(c => c.AverageRating > rating);
            
            return filteredCourses.ToList();
        }

        public async Task<List<CourseEntity>> GetCoursesSortedByRating(bool isDescending, CancellationToken cancellationToken)
        {
            var courses = await dbContext.Courses
                .AsNoTracking()
                .Include(t => t.Trainings)
                .Include(s => s.Subscribers)
                .ToListAsync(cancellationToken);

            var sortedCourses = isDescending
                ? courses.OrderByDescending(x => x.AverageRating)
                : courses.OrderBy(x => x.AverageRating);

            return sortedCourses.ToList();
        }

        public async Task UpdateCourse(CourseEntity course, CancellationToken cancellationToken)
        {
            dbContext.Courses.Update(course);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task RateCourse(Guid courseId, int rating, CancellationToken cancellationToken)
        {
            var course = await dbContext.Courses.FindAsync(courseId, cancellationToken);
            
            course.Ratings.Add(rating);
            
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task AddTrainingToCourse(Guid courseId, Guid trainingId, CancellationToken cancellationToken)
        {
            var course = await dbContext.Courses.FindAsync(courseId, cancellationToken);
            var training = await dbContext.Trainings.FindAsync(trainingId, cancellationToken);
            
            course.Trainings.Add(training);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task RemoveTrainingFromCourse(Guid courseId, Guid trainingId, CancellationToken cancellationToken)
        {
            var course = await dbContext.Courses
                .Include(c => c.Trainings)
                .FirstOrDefaultAsync(c => c.Id == courseId, cancellationToken);
            
            var training = await dbContext.Trainings.FindAsync(trainingId);
            
            course.Trainings.Remove(training);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task SubscribeToCourse(Guid courseId, Guid userId, CancellationToken cancellationToken)
        {
            var course = await dbContext.Courses.FindAsync(courseId, cancellationToken);
            var subscriber = await dbContext.Users.FindAsync(userId, cancellationToken);
            
            course.Subscribers.Add(subscriber);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UnsubscribeFromCourse(Guid courseId, Guid userId, CancellationToken cancellationToken)
        {
            var course = await dbContext.Courses
                .Include(c => c.Subscribers)
                .FirstOrDefaultAsync(c => c.Id == courseId, cancellationToken);
            
            var subscriber = await dbContext.Users.FindAsync(userId);
            
            course.Subscribers.Remove(subscriber);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
