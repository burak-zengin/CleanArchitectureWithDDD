using Domain.Products;
using Domain.Results;
using MediatR;

namespace Application.Products.Create;

public class Handler(IProductRepository repository) : IRequestHandler<Command, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(Command request, CancellationToken cancellationToken)
    {
        var productResult = Product.Create(Guid.NewGuid(), request.ModelCode);

        if (productResult.Failed)
        {
            return Result<Guid>.Fail(productResult.Messages);
        }

        var any = await repository.AnyAsync(productResult.Data.ModelCode, cancellationToken);

        if (any)
        {
            return Result<Guid>.Fail(["Model Code exist."]);
        }

        foreach (var theProductItem in request.ProductItems)
        {
            var productItemResult = Domain.Products.ProductItem.Create(
                Guid.NewGuid(),
                productResult.Data.Id,
                theProductItem.Barcode,
                theProductItem.Color,
                theProductItem.Size,
                theProductItem.Currency,
                theProductItem.Amount);

            if (productItemResult.Failed)
            {
                return Result<Guid>.Fail(productItemResult.Messages);
            }

            var addProductResult = productResult.Data.AddProduct(productItemResult.Data);

            if (addProductResult.Failed)
            {
                return Result<Guid>.Fail(addProductResult.Messages);
            }
        }

        await repository.CreateAsync(productResult.Data, cancellationToken);

        return Result<Guid>.Success(productResult.Data.Id.Value);
    }
}
