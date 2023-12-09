using MediatR;

namespace Morgly.Application.Features.AddTermToMortgage;

public class NewMortgageTermCommand : IRequest
{
    public int LengthInMonths { get; }
    public decimal InterestRate { get; }
    public Guid MortgageId { get; }
    public DateTime StartDate { get; }

    public NewMortgageTermCommand(Guid mortgageId, DateTime startDate, int lengthInMonths, decimal interestRate)
    {
        LengthInMonths = lengthInMonths;
        InterestRate = interestRate;
        MortgageId = mortgageId;
        StartDate = startDate;
    }
}