using Domain.Results;

namespace Domain.Products;

public class ProductItem
{
    public ProductItemId Id { get; init; }

    public ProductId ProductId { get; init; }

    public Barcode Barcode { get; init; }

    public string Color { get; init; }

    public string Size { get; init; }

    public Money Price { get; init; }

    private ProductItem() { }

    private ProductItem(ProductItemId id, ProductId productId, Barcode barcode, string color, string size, Money price)
    {
        Id = id;
        ProductId = productId;
        Barcode = barcode;
        Color = color;
        Size = size;
        Price = price;
    }

    public static Result<ProductItem> Create(Guid id, ProductId productId, string barcode, string color, string size, string currency, decimal amount)
    {
        var productItemIdResult = ProductItemId.Create(id);

        if (productItemIdResult.Failed)
        {
            return Result<ProductItem>.Fail(productItemIdResult.Messages);
        }

        var moneyResult = Money.Create(currency, amount);

        if (moneyResult.Failed)
        {
            return Result<ProductItem>.Fail(moneyResult.Messages);
        }

        var barcodeResult = Barcode.Create(barcode);

        if (barcodeResult.Failed)
        {
            return Result<ProductItem>.Fail(barcodeResult.Messages);
        }

        if (string.IsNullOrEmpty(color))
        {
            return Result<ProductItem>.Fail(["Color cannot be null or empty."]);
        }

        if (string.IsNullOrEmpty(size))
        {
            return Result<ProductItem>.Fail(["Size cannot be null or empty."]);
        }

        return Result<ProductItem>.Success(new ProductItem(productItemIdResult.Data, productId, barcodeResult.Data, color, size, moneyResult.Data));
    }
}