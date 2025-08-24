namespace MeowCore.Helpers
{
    public class ApiResponse<T>
    {
        public int Code { get; set; }
        public T? Value { get; set; }
        public string Message { get; set; } = string.Empty;
        public bool IsSuccess { get; set; }

        public static ApiResponse<T> Success(T value, string message = "", int code = 200)
        {
            return new ApiResponse<T>
            {
                Code = code,
                Value = value,
                Message = message,
                IsSuccess = true
            };
        }

        public static ApiResponse<T> Fail(string message, int code = 500)
        {
            return new ApiResponse<T>
            {
                Code = code,
                Value = default,
                Message = message,
                IsSuccess = false
            };
        }
    }
}
