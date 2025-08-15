namespace GymMateApi.Contracts;

public class CreateCommentRequest
{
    public string Text { get; set; } = string.Empty;
    public Guid AuthorId { get; set; }
    public Guid TrainingId { get; set; }
}