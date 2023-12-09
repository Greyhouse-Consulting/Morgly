using MongoDB.Bson.Serialization.Attributes;
using Morgly.Domain.Entities;
using Morgly.Domain.Events;

namespace Morgly.Domain.IntegrationEvents;


public interface IIntegrationEvent
{

}

public class IntegrationEventContainer
{
    public Guid Id { get; set; }
    public Status Status { get; set; }
    public Guid TransactionId { get; set; }

    public string Payload { get; set; } = string.Empty;
    public string PayloadType { get; set; } = string.Empty;
    public void MarkAsSent()
    {
        Status = Status.Sent;
    }
}

public interface NewMortgageTermsEvent: IIntegrationEvent

{

    public int LenghtInMonths { get; set; }
    public DateTime StartDate { get; set; }
}

public interface NewMortgageEvent : IIntegrationEvent

{
    public decimal Amount { get; set; }
}


public interface ApplicationCreatedEvent : IIntegrationEvent
{
    public Guid ApplicationId { get; }
    public decimal Amount { get; }
}
