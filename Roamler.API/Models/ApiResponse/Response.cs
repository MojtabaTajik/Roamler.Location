using System.Text.Json.Serialization;

namespace Roamler.API.Models.ApiResponse;

public class Response : IResponse<object>
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ResponseStatus Status { get; set; }
    public List<string> Errors { get; set; }
    public string Message { get; set; }
    public object Data { get; set; }
}