using MediatR;
using Morgly.Application.Interfaces;
using Morgly.Domain.Repositories;
using Morgly.Application.IntegrationEvents;
using Morgly.Domain.Entities;


namespace Morgly.Application.Features.Mortgage.Command;


public class CreateMortgageCommand(string name, List<MortgageSection> sections)
    : IRequest<Guid>
{
    public List<MortgageSection> sections { get; } = sections;
}

public interface IMortgageCreatedEvent
{
    Guid MortgageId { get; }
    decimal Amount { get; } // Added property for Amount
}

public class NewMortgageRequest
{
    public List<MortgageSection> Sections { get; set; }
}
public record MortgageSection(Guid id,  decimal mortgageAmount);

public class MortgageCreatedEvent(Guid mortgageId, decimal amount) : IMortgageCreatedEvent, INotification
{
    public Guid MortgageId { get; } = mortgageId;
    public decimal Amount { get; } = amount; // Added property for Amount
}

public class CreateMortgageCommandHandler(
    IUnitOfWork uow,
    IMediator mediator,
    IMortgageRepository mortgageRepository
    //IEventRepository eventRepository,
    //TransactionIdHolder transactionIdHolder
    )
    : IRequestHandler<CreateMortgageCommand, Guid>
{
    public async Task<Guid> Handle(CreateMortgageCommand request, CancellationToken cancellationToken)
    {
        await uow.BeginTransaction();
        try
        {
            foreach (var mortgageSection in request.sections)
            {
                if (mortgageSection.mortgageAmount > 1000000)
                {
                    throw new Exception("monthlyPayment cannot be greater than 1000000");
                }

                var startDate = DateTime.Now;
                var mortgage = new Domain.Entities.Mortgage(Guid.NewGuid(), mortgageSection.mortgageAmount, new Term(new TermDate(startDate.Year, startDate.Month), 12, 3.0m));

                await mortgageRepository.Add(mortgage);

                var mortgageCreatedEvent = new MortgageCreatedEvent(mortgage.Id, mortgageSection.mortgageAmount); // Updated constructor call to pass Amount

                await mediator.Publish(mortgageCreatedEvent, cancellationToken);

                //eventRepository.Add(new NewMortgageTermsEvent { TransactionId = transactionIdHolder.TransactionId });
            }
            await uow.SaveChanges();

        }
        catch (Exception)
        {
            await uow.AbortTransaction();
            throw;
        }
        return Guid.NewGuid();
    }
}

public class MortgageCreatedEventHandler : INotificationHandler<MortgageCreatedEvent>
{
    public Task Handle(MortgageCreatedEvent mortgageCreatedEvent, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}