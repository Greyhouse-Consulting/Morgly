using MediatR;
using Morgly.Application.Common;
using Morgly.Application.Interfaces;

namespace Morgly.Application.Features.AddTermToMortgage;


public class NewMortgageTermCommandHandler : IRequestHandler<NewMortgageTermCommand>
{
    private readonly IUnitOfWork _uow;
    private readonly IDomainEventDispatcher _dispatcher;

    public NewMortgageTermCommandHandler(IUnitOfWork uow, IDomainEventDispatcher dispatcher)
    {
        _uow = uow;
        _dispatcher = dispatcher;
    }

    public async Task Handle(NewMortgageTermCommand command, CancellationToken cancellationToken)
    {
        var mortgage = await _uow.Mortgages.Get(command.MortgageId);

        mortgage.AddTerm(command.StartDate, command.LengthInMonths, command.InterestRate);

        await _dispatcher.Dispatch(mortgage.DomainEvents, cancellationToken);
    }
}

