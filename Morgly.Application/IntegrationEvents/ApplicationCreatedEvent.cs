using Morgly.Domain.IntegrationEvents;

namespace Morgly.Application.IntegrationEvents;

public record ApplicationCreatedEvent {
    public Guid ApplicationId { get; set; } 
    public decimal Amount { get; set; } 
}