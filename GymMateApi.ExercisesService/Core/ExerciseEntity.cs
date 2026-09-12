namespace GymMateApi.ExercisesService.Core;

public class ExerciseEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public Guid TrainingId { get; set; }
}
