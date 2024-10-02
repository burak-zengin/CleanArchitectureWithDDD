using Domain.Products;
using MediatR;

namespace Application.Products.Create;

public class ProductCreatedEventHandler : INotificationHandler<ProductCreatedEvent>
{
    public Task Handle(ProductCreatedEvent notification, CancellationToken cancellationToken)
    {
        //TO DO:

        return Task.CompletedTask;
    }
}
