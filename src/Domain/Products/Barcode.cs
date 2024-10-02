using Domain.Results;

namespace Domain.Products;

public record Barcode
{
    public string Value { get; init; }

    public Barcode(string value)
    {
        Value = value;
    }

    public static Result<Barcode> Create(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return Result<Barcode>.Fail(["Barcode is not null or empty."]);
        }

        if (value.Length != 13)
        {
            return Result<Barcode>.Fail(["The barcode must consist of 13 digits."]);
        }

        return Result<Barcode>.Success(new Barcode(value));
    }
}
