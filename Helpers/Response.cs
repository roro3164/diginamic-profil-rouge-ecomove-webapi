namespace ecomove_back.Helpers
{
    public class Response<T>
    {
        public required T Data { get; set; }
        public required string Message { get; set; }
        public required bool IsSuccess { get; set; }
    }

    public class Response
    {
        public required string Message { get; set; }
        public required bool IsSuccess { get; set; }
    }

}
