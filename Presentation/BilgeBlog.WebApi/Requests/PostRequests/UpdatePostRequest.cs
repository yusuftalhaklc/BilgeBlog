namespace BilgeBlog.WebApi.Requests.PostRequests
{
    public class UpdatePostRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public bool IsPublished { get; set; }
    }
}

