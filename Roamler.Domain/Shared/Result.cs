namespace Roamler.Domain.Shared;

public class Result
{
    public bool IsSuccess { get; protected set; }
    public bool IsFailed { get; protected set; }

    public static Result Success() => new() { IsSuccess = true };
    public static Result Failed() => new() { IsFailed = true };
}

public class Result<TValue> : Result
{
    public Result(TValue data)
    {
        Data = data;
    }
    
    public TValue Data { get; set; }
    
    public static Result<TValue> Success(TValue data = default) => new(data) { IsSuccess = true };
    public static Result<TValue> Failed(TValue data = default) => new(data) { IsFailed = true };
}