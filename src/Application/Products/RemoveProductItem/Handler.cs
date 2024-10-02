using Domain.Products;
using Domain.Results;
using MediatR;

namespace Application.Products.RemoveProduct;

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

        var productItemIdResult = ProductItemId.Create(request.Id);

        if (productItemIdResult.Failed)
        {
            return Result.Fail(productItemIdResult.Messages);
        }

        if (product.ProductItems.Any(_ => _.Id == productItemIdResult.Data) == false)
        {
            return Result.Fail(["Product Item not found."]);
        }

        product.RemoveProduct(productItemIdResult.Data);

        await repository.UpdateAsync(product, cancellationToken);

        return Result.Success();
    }
}
