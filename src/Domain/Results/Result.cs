namespace Domain.Results;

public class Result
{
    public bool Failed { get; init; }

    public string[] Messages { get; init; }

    protected Result()
    {
    }

    public static Result Fail(string[] messages)
    {
        return new Result()
        {
            Failed = true,
            Messages = messages
        };
    }

    public static Result Success()
    {
        return new Result();
    }
}
