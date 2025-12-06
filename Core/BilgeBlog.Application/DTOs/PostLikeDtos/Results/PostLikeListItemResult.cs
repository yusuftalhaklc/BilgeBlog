namespace BilgeBlog.Application.DTOs.PostLikeDtos.Results
{
    public class PostLikeListItemResult
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
    }
}

