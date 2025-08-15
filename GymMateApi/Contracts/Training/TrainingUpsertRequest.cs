namespace GymMateApi.Contracts.Training;

public class TrainingUpsertRequest
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}