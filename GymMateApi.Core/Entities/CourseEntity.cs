using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMateApi.Core.Entities
{
    public class CourseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int[] Rating { get; set; } = [];

        public ICollection<UserEntity> Subscribers { get; set; } = [];
        public ICollection<TrainingEntity> Trainings { get; set; } = [];
    }
}
