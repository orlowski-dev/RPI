namespace Game.Core.Application.Results;

public partial class Result<T> : Result
{
    public T? Value { get; }

    public Result(T value)
        : base(true, null)
    {
        Value = value;
    }

    public static Result<T> Success(T value)
    {
        return new(value);
    }

    public static Result<T> Fail(T err)
    {
        return new(err);
    }
}
