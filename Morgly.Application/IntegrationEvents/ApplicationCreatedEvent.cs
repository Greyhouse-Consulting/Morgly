using Morgly.Domain.IntegrationEvents;

namespace Morgly.Application.IntegrationEvents;

public class ApplicationCreatedEvent(Guid applicationId, decimal amount) : Domain.IntegrationEvents.ApplicationCreatedEvent
{
    public Guid ApplicationId { get; } = applicationId;
    public decimal Amount { get; } = amount;
}