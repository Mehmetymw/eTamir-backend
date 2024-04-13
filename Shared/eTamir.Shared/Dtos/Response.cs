using System.Text.Json.Serialization;

namespace eTamir.Shared.Dtos
{
    public class Response<T> : IResponse<T>
    {
        public T? Data { get; set; }
        [JsonIgnore]
        public int StatusCode { get; set; }
        [JsonIgnore]
        public bool IsSuccessful { get; set; }
        public List<string> Errors { get; set; }


        public static Response<T> Fail(List<string> error, int statusCode)
        {
            return new Response<T>
            {
                Errors = error,
                IsSuccessful = false,
                StatusCode = statusCode
            };
        }
        public static Response<T> Fail(string error, int statusCode)
        {
            return Fail(new List<string> { error }, statusCode);
        }
        public static Response<T> Success( int statusCode, T? data = default)
        {
            return new Response<T>
            {
                Data = data,
                StatusCode = statusCode,
                IsSuccessful = true
            };
        }
    }
}
