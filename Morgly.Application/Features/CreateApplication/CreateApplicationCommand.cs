using MediatR;
using Microsoft.Extensions.Logging;
using Morgly.Application.Interfaces;
using Morgly.Domain.Repositories;

namespace Morgly.Application.Features.CreateApplication;

public class CreateApplicationCommand (decimal amount, DateTime startDate, string purpose) : IRequest<Guid>
{
    public decimal Amount { get; } = amount;
    public DateTime StartDate { get; } = startDate;
    public string Purpose { get; } = purpose;
}

public class CreateApplicationCommandHandler(ILogger<CreateApplicationCommandHandler> logger, IApplicationRepository applicationRepository, IUnitOfWork uow, IEventRepository eventRepository)
    : IRequestHandler<CreateApplicationCommand, Guid>
{
    public async Task<Guid> Handle(CreateApplicationCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating application");

        var application = new Domain.Entities.MortgageApplication { Id = Guid.NewGuid(), Status = "New", Amount = request.Amount};

        await applicationRepository.Add(application);

        await uow.SaveChanges(cancellationToken);

        var x = new IntegrationEvents.ApplicationCreatedEvent(application.Id, request.Amount);

        eventRepository.Add(x);

        return application.Id;
    }
}
