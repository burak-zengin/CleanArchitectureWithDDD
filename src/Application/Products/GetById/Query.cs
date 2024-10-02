using Domain.Products;
using Domain.Results;
using MediatR;

namespace Application.Products.GetById;

public record Query(Guid Id) : IRequest<Result<Product>>;