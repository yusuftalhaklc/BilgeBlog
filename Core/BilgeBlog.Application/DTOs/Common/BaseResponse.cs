namespace BilgeBlog.Application.DTOs.Common
{
    public record BaseResponse<T>
    {
        public T? Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;

        public static BaseResponse<T> Ok(T data, string message = "İşlem başarılı")
        {
            return new BaseResponse<T>
            {
                Success = true,
                Message = message,
                Data = data
            };
        }

        public static BaseResponse<T> Fail(string message = "İşlem başarısız")
        {
            return new BaseResponse<T>
            {
                Success = false,
                Message = message,
                Data = default
            };
        }
    }
}

