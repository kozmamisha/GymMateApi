using GymMateApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMateApi.Persistence.Interfaces
{
    public interface ICourseRepository
    {
        Task<List<CourseEntity>> GetAllCourses();
        Task<CourseEntity?> GetCourseById(Guid id);
        Task CreateCourse(CourseEntity course);
        Task UpdateCourse(CourseEntity course);
        Task DeleteCourse(CourseEntity course);
        Task SubscribeAsync(Guid courseId, Guid userId);
        Task UnsubscribeAsync(Guid courseId, Guid userId);
        Task RateCourseAsync(Guid courseId, int rating);
    }
}
