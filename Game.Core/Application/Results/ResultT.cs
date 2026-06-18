public partial class Result<T> : Result
{
    public T Value { get; }

    public Result(T value)
        : base(true, null)
    {
        Value = value;
    }

    public Result(Error error)
        : base(false, error)
    {
        Value = default!;
    }

    public static Result<T> Success(T value)
    {
        return new(value);
    }

    public static new Result<T> Fail(Error err)
    {
        return new(err);
    }
}
