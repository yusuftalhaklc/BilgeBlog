namespace BilgeBlog.Application.DTOs.CommentDtos.Results
{
    public class CommentResult
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public Guid PostId { get; set; }
        public string PostTitle { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public Guid? ParentCommentId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}

