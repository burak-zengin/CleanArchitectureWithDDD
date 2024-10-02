using Domain.Results;
using System.Text.RegularExpressions;

namespace Domain.Products;

public record ModelCode
{
    private const int Length = 9;

    public string Value { get; init; }

    private ModelCode(string value)
    {
        this.Value = value;
    }

    public static Result<ModelCode> Create(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return Result<ModelCode>.Fail(["Value cannot be null or empty."]);
        }

        var match = Regex.Match(value, "^\\d{2}[A-Za-z]\\d{6}$");
        if (match.Success == false)
        {
            return Result<ModelCode>.Fail(["Value must consist of 9 characters. Example: 24Y000001"]);
        }

        return Result<ModelCode>.Success(new ModelCode(value));
    }
}
