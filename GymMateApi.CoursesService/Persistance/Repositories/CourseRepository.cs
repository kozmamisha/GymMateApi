using GymMateApi.CoursesService.Core;
using GymMateApi.CoursesService.Persistance.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymMateApi.CoursesService.Persistance.Repositories;

public class CourseRepository(CoursesDbContext dbContext) : ICourseRepository
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
            .OrderBy(c => c.Id)
            .ToListAsync(cancellationToken);
    }

    public async Task<CourseEntity?> GetCourseById(Guid id, CancellationToken cancellationToken)
    {
        return await dbContext.Courses
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<List<CourseEntity>> GetCoursesByRatingFilter(int rating, CancellationToken cancellationToken)
    {
        var courses = await dbContext.Courses
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var filteredCourses = courses.Where(c => c.AverageRating > rating);

        return filteredCourses.ToList();
    }

    public async Task<List<CourseEntity>> GetCoursesSortedByRating(bool isDescending,
        CancellationToken cancellationToken)
    {
        var courses = await dbContext.Courses
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var sortedCourses = isDescending
            ? courses.OrderByDescending(c => c.AverageRating)
            : courses.OrderBy(c => c.AverageRating);

        return sortedCourses.ToList();
    }

    public async Task UpdateCourse(CourseEntity course, CancellationToken cancellationToken)
    {
        dbContext.Courses.Update(course);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task RateCourse(Guid courseId, int rating, CancellationToken cancellationToken)
    {
        var course = await GetTrackedCourse(courseId, cancellationToken);

        course.Ratings.Add(rating);

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task AddTrainingToCourse(Guid courseId, Guid trainingId, CancellationToken cancellationToken)
    {
        var course = await GetTrackedCourse(courseId, cancellationToken);

        course.TrainingIds.Add(trainingId);

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveTrainingFromCourse(Guid courseId, Guid trainingId, CancellationToken cancellationToken)
    {
        var course = await GetTrackedCourse(courseId, cancellationToken);

        course.TrainingIds.Remove(trainingId);

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task SubscribeToCourse(Guid courseId, Guid userId, CancellationToken cancellationToken)
    {
        var course = await GetTrackedCourse(courseId, cancellationToken);

        course.SubscriberIds.Add(userId);

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UnsubscribeFromCourse(Guid courseId, Guid userId, CancellationToken cancellationToken)
    {
        var course = await GetTrackedCourse(courseId, cancellationToken);

        course.SubscriberIds.Remove(userId);

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task<CourseEntity> GetTrackedCourse(Guid courseId, CancellationToken cancellationToken)
    {
        return await dbContext.Courses
            .FirstAsync(c => c.Id == courseId, cancellationToken);
    }
}
