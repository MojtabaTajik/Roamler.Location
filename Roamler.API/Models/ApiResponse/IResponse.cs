namespace Roamler.API.Models.ApiResponse;

public interface IResponse<T>
{ 
    ResponseStatus Status { get; set; }
    string Message { get; set; }
    T Data { get; set; }
}