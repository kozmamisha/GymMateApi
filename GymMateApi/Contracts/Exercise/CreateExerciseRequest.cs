namespace GymMateApi.Contracts.Exercise;

public class CreateExerciseRequest
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid TrainingId { get; set; }
}