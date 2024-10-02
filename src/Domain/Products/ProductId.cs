using Domain.Results;

namespace Domain.Products;

public record ProductId
{
    public Guid Value { get; init; }

    private ProductId(Guid value)
    {
        Value = value;
    }

    public static Result<ProductId> Create(Guid value)
    {
        if (value == Guid.Empty)
        {
            return Result<ProductId>.Fail(["Value cannot be empty."]);
        }

        return Result<ProductId>.Success(new ProductId(value));
    }
}
