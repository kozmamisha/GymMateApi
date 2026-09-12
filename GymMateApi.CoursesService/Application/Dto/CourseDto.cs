namespace GymMateApi.CoursesService.Application.Dto;

public class CourseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double AverageRating { get; set; }

    public ICollection<Guid> SubscriberIds { get; set; } = new List<Guid>();
    public ICollection<Guid> TrainingIds { get; set; } = new List<Guid>();
}
