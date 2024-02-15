using Morgly.Domain.Entities;

namespace Morgly.Domain.Events;

public class MortgageTermAddedEvent : DomainEvent
{
    public Guid MortgageId { get; }
    public TermDate TermStartDate { get; }
    public int LengthInMonths { get; }

    public MortgageTermAddedEvent(Guid mortgageId, TermDate termStartDate, int lengthInMonths)
    {
        MortgageId = mortgageId;
        TermStartDate = termStartDate;
        LengthInMonths = lengthInMonths;
    }
}