namespace BilgeBlog.Application.DTOs.PostLikeDtos.Results
{
    public class PostLikeResult
    {
        public Guid PostId { get; set; }
        public string PostTitle { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

