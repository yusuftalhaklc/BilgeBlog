namespace BilgeBlog.Application.DTOs.TagDtos.Results
{
    public class TagResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}

