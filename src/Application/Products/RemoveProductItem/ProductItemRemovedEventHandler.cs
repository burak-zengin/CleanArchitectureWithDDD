using Domain.Products;
using MediatR;

namespace Application.Products.RemoveProductItem;

public class ProductItemRemovedEventHandler : INotificationHandler<ProductItemRemovedEvent>
{
    public Task Handle(ProductItemRemovedEvent notification, CancellationToken cancellationToken)
    {
        //TO DO:

        return Task.CompletedTask;
    }
}
