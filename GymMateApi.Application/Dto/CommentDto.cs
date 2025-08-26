namespace GymMateApi.Application.Dto
{
    public class CommentDto
    {
        public Guid Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public Guid AuthorId { get; set; }
    }
}
