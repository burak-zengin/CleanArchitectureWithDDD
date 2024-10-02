namespace Domain;

public abstract class AggregateRoot
{
    private readonly List<IDomainEvent> _events = [];

    public IReadOnlyCollection<IDomainEvent> Events => _events;

    public void RaiseDomainEvent(IDomainEvent @event)
    {
        _events.Add(@event);
    }

    public void ClearDomainEvents()
    {
        _events.Clear();
    }
}
