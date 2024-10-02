using Domain.Results;
using MediatR;

namespace Application.Products.AddProductItem;

public record Command(Guid ProductId, string Barcode, string Color, string Size, string Currency, decimal Amount) : IRequest<Result>;
