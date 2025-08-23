namespace GymMateApi.Contracts;

public class CreateCommentRequest
{
    public string Text { get; set; } = string.Empty;
    public Guid TrainingId { get; set; }
}