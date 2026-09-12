using System.ComponentModel.DataAnnotations.Schema;

namespace GymMateApi.CoursesService.Core;

public class CourseEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<int> Ratings { get; set; } = [];

    [NotMapped]
    public double AverageRating => Ratings.Count > 0 ? double.Round(Ratings.Average(), 2) : 0;

    public List<Guid> SubscriberIds { get; set; } = [];
    public List<Guid> TrainingIds { get; set; } = [];
}
