using MediatR;

namespace Morgly.Application.Features.AddTermToMortgage;

public class NewMortgageTermCommand(Guid mortgageId, DateTime startDate, int lengthInMonths, decimal interestRate)
    : IRequest
{
    public int LengthInMonths { get; } = lengthInMonths;
    public decimal InterestRate { get; } = interestRate;
    public Guid MortgageId { get; } = mortgageId;
    public DateTime StartDate { get; } = startDate;
}