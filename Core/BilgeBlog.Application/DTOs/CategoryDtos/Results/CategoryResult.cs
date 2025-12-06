namespace BilgeBlog.Application.DTOs.CategoryDtos.Results
{
    public class CategoryResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}

