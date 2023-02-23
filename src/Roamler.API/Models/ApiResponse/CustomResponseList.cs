using System.Text.Json.Serialization;

namespace Roamler.API.Models.ApiResponse;


public class CustomResponseList<T> : IResponseList<T> where T : class
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ResponseStatus Status { get; set; }
    public string Message { get; set; }
    public int Count { get; set; }
    public List<T> Data { get; set; }
}