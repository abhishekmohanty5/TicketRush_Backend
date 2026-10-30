namespace TicketRush.Application.Common.DTOS
{
    public sealed class GlobalResponseDto<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }

        public static GlobalResponseDto<T> Ok(T data, string message = "Success")
        {
            return new GlobalResponseDto<T>
            {
                Success = true,
                Message = message,
                Data = data
            };
        }

        public static GlobalResponseDto<T> Fail(string message)
        {
            return new GlobalResponseDto<T>
            {
                Success = false,
                Message = message,
                Data = default
            };
        }
    }
}
