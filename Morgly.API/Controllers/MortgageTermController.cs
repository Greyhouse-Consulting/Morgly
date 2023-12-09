using MediatR;
using Microsoft.AspNetCore.Mvc;
using Morgly.Application.Features.AddTermToMortgage;

namespace Morgly.API.Controllers;

[ApiController]
[Route("/mortgages/{id}/terms")]
public class MortgageTermController : ControllerBase
{
    private readonly IMediator _mediator;

    public MortgageTermController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(Guid id, NewMortgageTermRequest request)
    {
        await _mediator.Send(new NewMortgageTermCommand(id, request.StartDate, request.LengthInMonths, request.InterestRate));

        return Ok();
    }
}

public class NewMortgageTermRequest
{
    public DateTime StartDate { get; set; }
    public int LengthInMonths { get; set; }
    public decimal InterestRate { get; set; }
}