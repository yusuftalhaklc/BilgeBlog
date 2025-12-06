namespace BilgeBlog.WebApi.Requests.PostRequests
{
    public class UpdatePostRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public bool IsPublished { get; set; }
        public Guid? CategoryId { get; set; }
        public List<string> Tags { get; set; } = new();
    }
}

