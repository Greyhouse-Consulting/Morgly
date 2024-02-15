using MediatR;
using Microsoft.AspNetCore.Mvc;
using Morgly.Application.Features.CreateApplication;
using Morgly.Application.Interfaces;

namespace Morgly.API.Controllers;

[ApiController]
[Route("[controller]")]

public class MortgageApplicationsController(IMediator mediator, IApplicationRepository applicationRepository) : Controller
{

    [HttpPost]
    public async Task<IActionResult> Create(MortgageApplicationRequest request)
    {
        var id = await mediator.Send(new CreateApplicationCommand(request.Amount, request.StartDate, request.Purpose));
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

public class MortgageApplicationRequest
{

    public decimal Amount { get; set; }
    public DateTime StartDate { get; set; }
    public string Purpose { get; set; }
}