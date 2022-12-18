namespace Share_Music.DTOs
{
    public interface IResponse
    {
        string Message { get; set; }
        string MessageType { get; set; }
        bool IsSuccess { get; set; }
    }

    public class Response<T> : IResponse
    {
        public T? Data { get; set; }
        public string Message { get; set; } = string.Empty;
        public string MessageType { get; set; } = string.Empty;
        public bool IsSuccess { get; set; }

        public static explicit operator Response<T>(IResponse v)
        {
            throw new NotImplementedException();
        }
    }

    public static class Response
    {
        public static Response<T> Success<T>(T data, string? message = null)
        {
            return new Response<T>
            {
                IsSuccess = true,
                Data = data,
                Message = message,
                MessageType = "success"
            };
        }

        public static IResponse Error(string? message = null)
        {
            return new Response<object>
            {
                IsSuccess = false,
                Message = message,
                MessageType = "error"
            };
        }
    }
}
        
