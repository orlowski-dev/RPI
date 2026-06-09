namespace Game.Core.Application.Results;

/// <summary>
/// Uniwersalny typ zwracania wyniku operacji.
/// </summary>
public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error? Error { get; }

    protected Result(bool success, Error? err)
    {
        IsSuccess = success;
        Error = err;
    }

    public static Result Success()
    {
        return new Result(true, null);
    }

    public static Result Fail(Error err)
    {
        return new Result(false, err);
    }
}
