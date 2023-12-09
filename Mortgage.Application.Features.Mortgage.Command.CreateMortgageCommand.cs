using MediatR;

namespace Mortgage.Application.Features.Mortgage.Command
{
    public class CreateMortgageCommand : IRequest<int>
    {
        public string Name { get; }
        public double InterestRate { get; }
        public double LoanAmount { get; }
        public int Term { get; }

        public CreateMortgageCommand(string name, double interestRate, double loanAmount, int term)
        {
            Name = name;
            InterestRate = interestRate;
            LoanAmount = loanAmount;
            Term = term;
        }
    }
}
