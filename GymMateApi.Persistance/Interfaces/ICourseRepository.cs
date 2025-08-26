using GymMateApi.Core.Entities;

namespace GymMateApi.Persistence.Interfaces
{
    public interface ICourseRepository
    {
        Task<List<CourseEntity>> GetAllCourses(CancellationToken cancellationToken);
        Task<CourseEntity?> GetCourseById(Guid id, CancellationToken cancellationToken);
        Task CreateCourse(CourseEntity course, CancellationToken cancellationToken);
        Task UpdateCourse(CourseEntity course, CancellationToken cancellationToken);
        Task DeleteCourse(CourseEntity course, CancellationToken cancellationToken);
        Task SubscribeToCourse(Guid courseId, Guid userId, CancellationToken cancellationToken);
        Task UnsubscribeFromCourse(Guid courseId, Guid userId, CancellationToken cancellationToken);
        Task RateCourse(Guid courseId, int rating, CancellationToken cancellationToken);
        Task AddTrainingToCourse(Guid courseId, Guid trainingId, CancellationToken cancellationToken);
        Task RemoveTrainingFromCourse(Guid courseId, Guid trainingId, CancellationToken cancellationToken);
        Task<List<CourseEntity>> GetCoursesByRatingFilter(int rating, CancellationToken cancellationToken);
        Task<List<CourseEntity>> GetCoursesSortedByRating(bool isDescending, CancellationToken cancellationToken);
    }
}
