namespace Roamler.Domain.Shared;

public class Result
{
    public bool IsSuccess { get; protected set; }
    public bool IsFailed { get; protected set; }
    public bool IsCached { get; set; }

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
    public bool IsCached { get; private init; }
    
    public static Result<TValue> Success(TValue data = default, bool cached = false) => new(data) { IsSuccess = true, IsCached = cached};
    public static Result<TValue> Failed(TValue data = default) => new(data) { IsFailed = true };
}