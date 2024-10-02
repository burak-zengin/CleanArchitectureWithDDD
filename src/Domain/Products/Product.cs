using Domain.Results;
using System.Collections.ObjectModel;

namespace Domain.Products;

public class Product : AggregateRoot
{
    public ProductId Id { get; init; }

    public ModelCode ModelCode { get; init; }

    private HashSet<ProductItem> _productItems = [];

    public IReadOnlyCollection<ProductItem> ProductItems => _productItems.ToList();

    private Product() { }

    private Product(ProductId id, ModelCode modelCode)
    {
        Id = id;
        ModelCode = modelCode;
    }

    public static Result<Product> Create(Guid id, string modelCode)
    {
        var mainProductIdResult = ProductId.Create(id);
        if (mainProductIdResult.Failed)
        {
            return Result<Product>.Fail(mainProductIdResult.Messages);
        }

        var modelCodeResult = ModelCode.Create(modelCode);
        if (modelCodeResult.Failed)
        {
            return Result<Product>.Fail(modelCodeResult.Messages);
        }

        var product = new Product(
                mainProductIdResult.Data,
                modelCodeResult.Data);

        product.RaiseDomainEvent(new ProductCreatedEvent());

        return Result<Product>.Success(product);
    }

    public Result AddProduct(ProductItem product)
    {
        if (_productItems.Any(p => p.Barcode == product.Barcode))
        {
            return Result.Fail(["Barcode exist."]);
        }

        if (_productItems.Any(p => p.Size == product.Size && p.Color == product.Color))
        {
            return Result.Fail(["Size and color exist."]);
        }

        _productItems.Add(product);

        RaiseDomainEvent(new ProductItemAddedEvent());

        return Result.Success();
    }

    public Result RemoveProduct(ProductItemId id)
    {
        var product = _productItems.FirstOrDefault(p => p.Id == id);

        if (product is null)
        {
            return Result.Fail(["Product not found."]);
        }

        _productItems.Remove(product);

        RaiseDomainEvent(new ProductItemRemovedEvent());

        return Result.Success();
    }
}