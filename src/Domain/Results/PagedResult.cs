namespace Domain.Results;

public class PagedResult<T> : Result<List<T>>
{
    public int Page { get; init; }

    public int Take { get; init; }

    public static PagedResult<T> Success(int page, int take, List<T> data)
    {
        return new PagedResult<T>
        {
            Page = page,
            Take = take,
            Data = data
        };
    }

    public static PagedResult<T> Fail(string[] messages)
    {
        return new PagedResult<T>
        {
            Failed = true,
            Messages = messages
        };
    }
}
