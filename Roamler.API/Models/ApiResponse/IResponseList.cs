namespace Roamler.API.Models.ApiResponse;

public interface IResponseList<T> where T : class
{ 
    ResponseStatus Status { get; set; }
    string Message { get; set; }
    int Count { get; set; }
    List<T> Data { get; set; }
}