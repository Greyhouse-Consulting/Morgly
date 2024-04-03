using MassTransit;
using MediatR;

using Morgly.Application.Interfaces;

namespace Morgly.Application.Features.ApplicationStatusUpdated;

public class ApplicationStatusConsumer(IApplicationRepository applicationRepository, IUnitOfWork uow) : IConsumer<ApplicationStatusEvent>
{
    public async Task Consume(ConsumeContext<ApplicationStatusEvent> context)
    {
        await uow.BeginTransaction();
        try
        {
            var application = await applicationRepository.Get(context.Message.ApplicationId);

            application.Status = context.Message.Status;

            await applicationRepository.Update(application);

            await uow.SaveChanges();
        }
        catch (Exception )
        {
            await uow.AbortTransaction();
            throw;
        }

        // Get price
        //if (context.Message.Status == "approved")
        //    await mediator.Send(new CreateMortgageCommand("adasd",1.0m, application.Amount, 12));
    }
}





public class ApplicationStatusEvent
{
    public Guid ApplicationId { get; set; }

    public string Status { get; set; } = Domain.Events.Status.New.ToString();
}