using Domain.Results;

namespace Domain.Products;

public record Money
{
    public string Currency { get; init; }

    public decimal Amount { get; init; }

    private Money(string currency, decimal amount)
    {
        Currency = currency;
        Amount = amount;
    }

    public static Result<Money> Create(string currency, decimal amount)
    {
        if (string.IsNullOrEmpty(currency))
        {
            return Result<Money>.Fail(["Currency is not null or empty."]);
        }

        if (currency.Length > 3)
        {
            return Result<Money>.Fail(["Currency must be a maximum of 3 characters."]);
        }

        if (amount == 0)
        {
            return Result<Money>.Fail(["Amount greater than '0'."]);
        }

        return Result<Money>.Success(new Money(currency, amount));
    }
}
