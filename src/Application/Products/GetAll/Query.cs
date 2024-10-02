using Domain.Products;
using Domain.Results;
using MediatR;

namespace Application.Products.GetAll;

public record Query(int Page, int Take) : IRequest<PagedResult<Product>>;
