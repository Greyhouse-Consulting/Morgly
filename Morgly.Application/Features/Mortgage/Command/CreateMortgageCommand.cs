using MediatR;
using Morgly.Application;
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

public class MortgageCreatedEvent : IMortgageCreatedEvent, INotification
{
    public Guid MortgageId { get; }
    public decimal Amount { get; } // Added property for amount

    public MortgageCreatedEvent(Guid mortgageId, decimal amount) // Updated constructor to accept amount
    {
        MortgageId = mortgageId;
        Amount = amount;
    }
}

public class CreateMortgageCommandHandler : IRequestHandler<CreateMortgageCommand, Guid>
{
    private readonly IUnitOfWork _uow;
    private readonly IMediator _mediator;
    private readonly IMortgageRepository _mortgageRepository;
    private readonly IEventRepository _eventRepository;
    private readonly TransactionIdHolder _transactionIdHolder;

    public CreateMortgageCommandHandler(IUnitOfWork uow, IMediator mediator, IMortgageRepository mortgageRepository, IEventRepository eventRepository, TransactionIdHolder transactionIdHolder)
    {
        _uow = uow;
        _mediator = mediator;
        _mortgageRepository = mortgageRepository;
        _eventRepository = eventRepository;
        _transactionIdHolder = transactionIdHolder;
    }

    public async Task<Guid> Handle(CreateMortgageCommand request, CancellationToken cancellationToken)
    {
        if (request.Amount > 1000000)
        {
            throw new Exception("monthlyPayment cannot be greater than 1000000");
        }

        var mortgage = new Domain.Entities.Mortgage(Guid.NewGuid(), request.Amount, new Term(DateTime.Now, 12, request.InterestRate));

        await _mortgageRepository.Add(mortgage);

        await _uow.SaveChanges(cancellationToken);

        var mortgageCreatedEvent = new MortgageCreatedEvent(mortgage.Id, request.Amount); // Updated constructor call to pass amount

        await _mediator.Publish(mortgageCreatedEvent, cancellationToken);

        _eventRepository.Add(new NewMortgageTermsEvent { TransactionId = _transactionIdHolder.TransactionId });

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