using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMateApi.Core.Entities
{
    public class TrainingEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public ICollection<CommentEntity> Comments { get; set; } = new List<CommentEntity>();

        public ICollection<ExerciseEntity> Exercises { get; set; } = new List<ExerciseEntity>();
        public ICollection<CourseEntity> Courses { get; set; } = new List<CourseEntity>();
    }
}
