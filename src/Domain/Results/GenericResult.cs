namespace Domain.Results;

public class Result<T> : Result
{
    public T Data { get; init; }

    public static Result<T> Success(T data)
    {
        return new Result<T>
        {
            Data = data
        };
    }

    public static Result<T> Fail(string[] messages)
    {
        return new Result<T>
        {
            Failed = true,
            Messages = messages
        };
    }
}
