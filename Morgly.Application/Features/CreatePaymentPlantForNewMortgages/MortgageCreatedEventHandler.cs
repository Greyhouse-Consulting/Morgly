using MassTransit;
using MediatR;
using Morgly.Domain.Events;

namespace Morgly.Application.Features.CreatePaymentPlantForNewMortgages;

public class MortgageCreatedEventHandler(IPublishEndpoint sendEndpointProvider) : INotificationHandler<MortgageTermAddedEvent>
{
    public  Task Handle(MortgageTermAddedEvent notification, CancellationToken cancellationToken)
    {

        return Task.CompletedTask;
        //await _sendEndpointProvider.Publish(new NewMortgageTermsEvent(), cancellationToken);
    }
}