namespace TaskManager.Domain;

public class Result
{
    protected Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None ||
            !isSuccess && error == Error.None)
        {
            throw new ArgumentException("Invalid error", nameof(error));
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public Error Error { get; }

    public static Result Success() => new(true, Error.None);

    public static Result Failure(string errorMessage) => new(false, new Error(errorMessage));
    public static Result Failure(Error error) => new Result(false, error);
}

public class Result<T> : Result
{
    public T Value { get; }

    protected Result(T value, bool isSuccess, Error error) : base(isSuccess, error)
    {
        Value = value;
    }

    public static Result<T> Success(T value) => new(value, true, Domain.Error.None);
    public static Result<T> Failure(string errorMessage) => new(default, false, new Error(errorMessage));
}

public record Error(string Description)
{
    public static readonly Error None = new(string.Empty);
}

public sealed record NotFound() : Error("Registro não encontrado.");