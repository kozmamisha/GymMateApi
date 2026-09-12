namespace GymMateApi.CommentsService.Presentation.Contracts.Comment;

public class CreateCommentRequest
{
    public string Text { get; set; } = string.Empty;
    public Guid TrainingId { get; set; }
}
