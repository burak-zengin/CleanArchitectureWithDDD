using AutoMapper;
using Domain.Products;
using Domain.Results;
using MediatR;

namespace Application.Products.GetAll;

public class Handler(IProductRepository repository, IMapper mapper) : IRequestHandler<Query, PagedResult<Product>>
{
    public async Task<PagedResult<Product>> Handle(Query request, CancellationToken cancellationToken)
    {
        var products = await repository.GetAllAsync(request.Page * request.Take, request.Take, cancellationToken);

        return PagedResult<Product>.Success(request.Page, request.Take, mapper.Map<List<Product>>(products));
    }
}
