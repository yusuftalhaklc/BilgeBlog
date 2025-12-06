namespace BilgeBlog.Application.DTOs.PostCategoryDtos.Results
{
    public class PostCategoryResult
    {
        public Guid PostId { get; set; }
        public string PostTitle { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

