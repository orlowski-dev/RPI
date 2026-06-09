namespace Game.Core.Application.Results;

public partial class Result<T> : Result
{
    public T? Value { get; }

    private Result(T value)
        : base(true, null)
    {
        Value = value;
    }

    private static Result<T> Success(T value)
    {
        return new(value);
    }

    private static Result<T> Fail(T err)
    {
        return new(err);
    }
}
