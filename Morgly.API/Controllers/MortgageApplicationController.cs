using MediatR;
using Microsoft.AspNetCore.Mvc;
using Morgly.Application.Features.CreateApplication;

namespace Morgly.API.Controllers;

[ApiController]
[Route("[controller]")]

public class MortgageApplicationController : Controller
{
    private readonly IMediator _mediator;

    public MortgageApplicationController(IMediator mediator)
    {
        _mediator = mediator;
    }
    // GET
    [HttpPost]
    public async Task<IActionResult> Create(MortgageApplicationRequest request)
    {
        var id = await _mediator.Send(new CreateApplicationCommand(request.Amount, request.StartDate, request.Purpose));
        return Ok(id);
    }
}

public class MortgageApplicationRequest
{

    public decimal Amount { get; set; }
    public DateTime StartDate { get; set; }
    public string Purpose { get; set; }
}