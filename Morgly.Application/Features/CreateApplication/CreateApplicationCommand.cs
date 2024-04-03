using MassTransit;
using MassTransit.MongoDbIntegration;
using MediatR;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Morgly.Application.IntegrationEvents;
using Morgly.Application.Interfaces;
using Morgly.Domain.Entities;
using Morgly.Domain.Repositories;

namespace Morgly.Application.Features.CreateApplication;

public class CreateApplicationCommand(decimal amount, DateTime startDate, string propertyId) : IRequest<Guid>
{
    public decimal Amount { get; } = amount;
    public DateTime StartDate { get; } = startDate;
    public string propertyId { get; } = propertyId;
}

public class CreateApplicationCommandHandler(ILogger<CreateApplicationCommandHandler> logger,  IApplicationRepository applicationRepository, IUnitOfWork uow, IPublishEndpoint publishEndpoint )
    : IRequestHandler<CreateApplicationCommand, Guid>
{
    public async Task<Guid> Handle(CreateApplicationCommand request, CancellationToken cancellationToken)
    {
        await uow.BeginTransaction();
        
        logger.LogInformation("Creating application");

        var application = new MortgageApplication { Id = Guid.NewGuid(), Status = "New", Amount = request.Amount, ProperyId = request.propertyId };

        try
        {
            await applicationRepository.Add(application);

            var x = new ApplicationCreatedEvent { Amount = request.Amount, ApplicationId = application.Id };

            await publishEndpoint.Publish(x, uow.Cts.Token);
            await uow.SaveChanges();
        }
        catch (Exception e)
        {

            await uow.AbortTransaction();
            throw;
        }

        return application.Id;
    }
}
