using System.ComponentModel.DataAnnotations;

namespace Ecomove.Api.Helpers
{
    public class Response<T>
    {
        public string? Message { get; set; }
        public T? Data { get; set; }
        public required bool IsSuccess { get; set; }
        public int CodeStatus { get; set; }
    }
}

