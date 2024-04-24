namespace ecomove_back.Helpers
{
    public class Response<T>
    {
        public T? Data { get; set; }
        public required string Message { get; set; }
        public required bool IsSuccess { get; set; }
    }
}