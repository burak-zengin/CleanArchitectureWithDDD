using Domain.Results;
using MediatR;

namespace Application.Products.Create;

public record Command(string ModelCode, List<ProductItem> ProductItems) : IRequest<Result<Guid>>;