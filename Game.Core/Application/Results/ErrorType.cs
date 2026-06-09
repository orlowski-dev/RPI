namespace Game.Core.Application.Results;

public enum ErrorType
{
    Validation,
    NotFound,
    Conflict,
    Unauthorized,
    InvalidState,
    Unknown,
}
