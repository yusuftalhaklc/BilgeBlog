using BilgeBlog.Application.DTOs.CategoryDtos.Results;
using BilgeBlog.Application.DTOs.TagDtos.Results;

namespace BilgeBlog.Application.DTOs.PostDtos.Results
{
    public class PostResult
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Content { get; set; }
        public bool IsPublished { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public List<TagResult> Tags { get; set; } = new();
        public CategoryResult Category { get; set; } = new();
        public int TotalLikeCount { get; set; }
        public int TotalCommentCount { get; set; }
        public bool IsLiked { get; set; }
    }
}

