using Domain.Results;
using MediatR;

namespace Application.Products.RemoveProduct;

public record Command(Guid Id, Guid ProductId) : IRequest<Result>;
