namespace BilgeBlog.Application.DTOs.CommentDtos.Results
{
    public class CommentListItemResult
    {
        public Guid Id { get; set; }
        public string Message { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public Guid? ParentCommentId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}

