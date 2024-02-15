using MediatR;
using Morgly.Application.Interfaces;
using Morgly.Domain.Repositories;
using Morgly.Application.IntegrationEvents;
using Morgly.Domain.Entities;


namespace Morgly.Application.Features.Mortgage.Command;


public class CreateMortgageCommand(string name, decimal interestRate, decimal amount, int termInMonths)
    : IRequest<Guid>
{
    public string Name { get; } = name;
    public decimal InterestRate { get; } = interestRate;
    public decimal Amount { get; } = amount;
    public int TermInMonths { get; } = termInMonths;
}

public interface IMortgageCreatedEvent
{
    Guid MortgageId { get; }
    decimal Amount { get; } // Added property for amount
}

public class MortgageCreatedEvent(Guid mortgageId, decimal amount) : IMortgageCreatedEvent, INotification
{
    public Guid MortgageId { get; } = mortgageId;
    public decimal Amount { get; } = amount; // Added property for amount
}

public class CreateMortgageCommandHandler(
    IUnitOfWork uow,
    IMediator mediator,
    IMortgageRepository mortgageRepository,
    IEventRepository eventRepository,
    TransactionIdHolder transactionIdHolder)
    : IRequestHandler<CreateMortgageCommand, Guid>
{
    public async Task<Guid> Handle(CreateMortgageCommand request, CancellationToken cancellationToken)
    {
        if (request.Amount > 1000000)
        {
            throw new Exception("monthlyPayment cannot be greater than 1000000");
        }

        var startDate = DateTime.Now;
        var mortgage = new Domain.Entities.Mortgage(Guid.NewGuid(), request.Amount, new Term(new TermDate(startDate.Year, startDate.Month), 12, request.InterestRate));

        await mortgageRepository.Add(mortgage);

        await uow.SaveChanges(cancellationToken);

        var mortgageCreatedEvent = new MortgageCreatedEvent(mortgage.Id, request.Amount); // Updated constructor call to pass amount

        await mediator.Publish(mortgageCreatedEvent, cancellationToken);

        eventRepository.Add(new NewMortgageTermsEvent { TransactionId = transactionIdHolder.TransactionId });

        return mortgage.Id;
    }
}

public class MortgageCreatedEventHandler : INotificationHandler<MortgageCreatedEvent>
{
    public Task Handle(MortgageCreatedEvent mortgageCreatedEvent, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}