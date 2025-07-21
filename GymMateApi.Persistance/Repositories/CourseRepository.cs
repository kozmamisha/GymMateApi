using GymMateApi.Core.Entities;
using GymMateApi.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMateApi.Persistence.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        public Task CreateCourse(CourseEntity course)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCourse(CourseEntity course)
        {
            throw new NotImplementedException();
        }

        public Task<List<CourseEntity>> GetAllCourses()
        {
            throw new NotImplementedException();
        }

        public Task<CourseEntity?> GetCourseById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task RateCourseAsync(Guid courseId, int rating)
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

        public Task UpdateCourse(CourseEntity course)
        {
            throw new NotImplementedException();
        }
    }
}
