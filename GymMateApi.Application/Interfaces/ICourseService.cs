using GymMateApi.Application.Dto;

namespace GymMateApi.Application.Interfaces;

public interface ICourseService
{
    Task<List<CourseDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<CourseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task CreateAsync(string name, CancellationToken cancellationToken);
    Task UpdateAsync(Guid id, string name, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    Task AddTrainingToCourseAsync(Guid courseId, Guid trainingId, CancellationToken cancellationToken);
    Task RemoveTrainingFromCourseAsync(Guid courseId, Guid trainingId, CancellationToken cancellationToken);
    Task RateCourseAsync(Guid courseId, int rating, CancellationToken cancellationToken);
    Task<List<CourseDto>> GetCoursesByRatingFilterAsync(int rating, CancellationToken cancellationToken);
    Task<List<CourseDto>> GetCoursesSortedByRatingAsync(bool isDescending, CancellationToken cancellationToken);
    Task SubscribeToCourse(Guid courseId, CancellationToken cancellationToken);
    Task UnsubscribeFromCourse(Guid courseId, CancellationToken cancellationToken);
}