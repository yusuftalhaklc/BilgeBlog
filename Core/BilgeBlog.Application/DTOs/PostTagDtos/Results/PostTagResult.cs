namespace BilgeBlog.Application.DTOs.PostTagDtos.Results
{
    public class PostTagResult
    {
        public Guid PostId { get; set; }
        public string PostTitle { get; set; }
        public Guid TagId { get; set; }
        public string TagName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

