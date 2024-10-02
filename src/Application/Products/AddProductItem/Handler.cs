using Domain.Products;
using Domain.Results;
using MediatR;

namespace Application.Products.AddProductItem;

public class Handler(IProductRepository repository) : IRequestHandler<Command, Result>
{
    public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
    {
        var productIdResult = ProductId.Create(request.ProductId);

        if (productIdResult.Failed)
        {
            return Result.Fail(productIdResult.Messages);
        }

        var product = await repository.GetAsync(productIdResult.Data, cancellationToken);

        if (product == null)
        {
            return Result.Fail(["Product not found."]);
        }

        var productItemResult = ProductItem.Create(
            Guid.NewGuid(),
            productIdResult.Data,
            request.Barcode,
            request.Color,
            request.Size,
            request.Currency,
            request.Amount);

        if (productItemResult.Failed)
        {
            return Result.Fail(productItemResult.Messages);
        }

        var any = await repository.AnyAsync(productItemResult.Data.Barcode, cancellationToken);

        if (any)
        {
            return Result.Fail(["Barcode exist."]);
        }

        var addProductResult = product.AddProduct(productItemResult.Data);

        if (addProductResult.Failed)
        {
            return Result.Fail(addProductResult.Messages);
        }

        await repository.UpdateAsync(product, cancellationToken);

        return Result.Success();
    }
}
