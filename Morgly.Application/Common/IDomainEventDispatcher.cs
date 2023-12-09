using MediatR;
using Morgly.Domain.Entities;

namespace Morgly.Application.Common;

public interface IDomainEventDispatcher
{
    public Task Dispatch(IEnumerable<DomainEvent> domainEvents, CancellationToken cancellationToken);
}

public class DomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IMediator _mediator;

    public DomainEventDispatcher(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Dispatch(IEnumerable<DomainEvent> domainEvents, CancellationToken cancellationToken)
    {
        foreach (var domainEvent in domainEvents)
        {
            await _mediator.Publish(domainEvent, cancellationToken);
        }
    }
}