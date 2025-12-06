namespace BilgeBlog.Application.DTOs.RoleDtos.Results
{
    public class RoleResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}

