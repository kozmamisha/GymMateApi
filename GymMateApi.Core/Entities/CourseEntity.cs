using System.ComponentModel.DataAnnotations.Schema;

namespace GymMateApi.Core.Entities
{
    public class CourseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<int> Ratings { get; set; } = [];
        
        [NotMapped]
        public double AverageRating => Ratings.Count > 0 ? double.Round(Ratings.Average(), 2) : 0;
        public ICollection<UserEntity> Subscribers { get; set; } = new List<UserEntity>();
        public ICollection<TrainingEntity> Trainings { get; set; } = new List<TrainingEntity>();
    }
}
