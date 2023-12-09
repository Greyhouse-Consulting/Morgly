// ReSharper disable once CheckNamespace

using Morgly.Domain.IntegrationEvents;

namespace Morgly.Application.IntegrationEvents;

public class NewMortgageTermsEvent : IntegrationEventContainer, Domain.IntegrationEvents.NewMortgageTermsEvent
{
    public int LenghtInMonths { get; set; }
    public DateTime StartDate { get; set; }
}

public class NewMortgageEvent : IntegrationEventContainer, Domain.IntegrationEvents.NewMortgageEvent
{
    public decimal Amount { get; set; }
}