namespace BilgeBlog.Application.DTOs.UserDtos.Results
{
    public class LoginResult
    {
        public UserResult User { get; set; } = null!;
        public string Token { get; set; } = string.Empty;
    }
}

