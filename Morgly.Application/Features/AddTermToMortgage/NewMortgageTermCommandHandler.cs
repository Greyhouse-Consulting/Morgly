using MediatR;
using Morgly.Application.Common;
using Morgly.Application.Interfaces;

namespace Morgly.Application.Features.AddTermToMortgage;


public class NewMortgageTermCommandHandler(IUnitOfWork uow)
    : IRequestHandler<NewMortgageTermCommand>
{
    public async Task Handle(NewMortgageTermCommand command, CancellationToken cancellationToken)
    {
        var mortgage = await uow.Mortgages.Get(command.MortgageId);

        mortgage.AddTerm(new DateOnly(command.StartDate.Year, command.StartDate.Month, command.StartDate.Day), command.LengthInMonths, command.InterestRate);

        await uow.Mortgages.Update(mortgage);

    }
}

