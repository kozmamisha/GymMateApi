using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMateApi.Core.Entities
{
    public class ExerciseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public Guid TrainingId { get; set; }
        public TrainingEntity? Training { get; set; }
    }
}
