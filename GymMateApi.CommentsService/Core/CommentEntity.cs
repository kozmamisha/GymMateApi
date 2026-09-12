namespace GymMateApi.CommentsService.Core;

public class CommentEntity
{
    public Guid Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

    public Guid AuthorId { get; set; }

    public Guid TrainingId { get; set; }
}
