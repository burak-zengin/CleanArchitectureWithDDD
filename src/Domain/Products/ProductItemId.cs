using Domain.Results;

namespace Domain.Products;

public record ProductItemId
{
    public Guid Value { get; init; }

    private ProductItemId(Guid value)
    {
        Value = value;
    }

    public static Result<ProductItemId> Create(Guid value)
    {
        if (value == Guid.Empty)
        {
            return Result<ProductItemId>.Fail(["Value cannot be empty."]);
        }

        return Result<ProductItemId>.Success(new ProductItemId(value));
    }
}
