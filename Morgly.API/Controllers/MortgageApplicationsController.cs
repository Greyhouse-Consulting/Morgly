using MassTransit;
using MassTransit.MongoDbIntegration;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Morgly.Application.Features.CreateApplication;
using Morgly.Application.Interfaces;
using Morgly.Domain.Entities;

namespace Morgly.API.Controllers;

[ApiController]
[Route("[controller]")]

public class MortgageApplicationsController(IMediator mediator, IApplicationRepository applicationRepository)
    : ControllerBase
{
 
    [HttpPost]
    public async Task<IActionResult> Create(MortgageApplicationRequest request)
    {

        //using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));
        ////await uow.BeginTransaction();

        //var application = new Domain.Entities.MortgageApplication { Id = Guid.NewGuid(), Status = "New", Amount = request.Amount, ProperyId = request.PropertyId };
        
        //await _context.BeginTransaction(cts.Token);

        ////await applicationRepository.Add(application);
        ////await _coll.InsertOneAsync(_context.Session, application, null, cts.Token);

        ////var x = new Application.IntegrationEvents.ApplicationCreatedEvent { ApplicationId = application.Id,  Amount = request.Amount };

        //await _publishEndpoint.Publish<TestEvent>(new { ApplicationId = application.Id,  Amount = request.Amount }, 
        //    cts.Token);

        //await _context.CommitTransaction(cts.Token);
        var id = await mediator.Send(new CreateApplicationCommand(request.Amount, request.StartDate, request.PropertyId));
        return Ok(id);
    }

    // GET
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var applications = await applicationRepository.GetAll();
        return Ok(applications);
    }
}
public record TestEvent {
    public Guid ApplicationId { get; set; } 
    public decimal Amount { get; set; } 
}

public class MortgageApplicationRequest
{

    public decimal Amount { get; set; }
    public DateTime StartDate { get; set; }
    public string PropertyId { get; set; }
}