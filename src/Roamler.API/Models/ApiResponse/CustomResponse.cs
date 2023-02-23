using System.Text.Json.Serialization;

namespace Roamler.API.Models.ApiResponse;

public class CustomResponse<T> : IResponse<T>
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ResponseStatus Status { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
    public long ResultCount { get; set; }
}