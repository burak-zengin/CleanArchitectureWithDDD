using Domain.Products;
using MediatR;

namespace Application.Products.AddProductItem;

public class ProductItemAddedEventHandler : INotificationHandler<ProductItemAddedEvent>
{
    public Task Handle(ProductItemAddedEvent notification, CancellationToken cancellationToken)
    {
        //TO DO:

        return Task.CompletedTask;
    }
}
