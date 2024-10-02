using AutoMapper;
using Domain.Products;
using Domain.Results;
using MediatR;

namespace Application.Products.GetById;

public class Handler(IProductRepository repository, IMapper mapper) : IRequestHandler<Query, Result<Product>>
{
    public async Task<Result<Product>> Handle(Query request, CancellationToken cancellationToken)
    {
        var productIdResult = ProductId.Create(request.Id);

        if (productIdResult.Failed)
        {
            return Result<Product>.Fail(productIdResult.Messages);
        }

        var product = await repository.GetAsync(productIdResult.Data, cancellationToken);

        if (product is null)
        {
            return Result<Product>.Fail(["Product not found."]);
        }

        return Result<Product>.Success(mapper.Map<Product>(product));
    }
}
