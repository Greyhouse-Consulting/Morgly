using MediatR;
using Microsoft.AspNetCore.Mvc;
using Morgly.Application.Features.AddTermToMortgage;
using Morgly.Application.Interfaces;

namespace Morgly.API.Controllers;

[ApiController]
[Route("/mortgages/{id}/terms")]
[Tags("mortgage-terms")]
public class MortgageTermController(IMediator mediator, IMortgageRepository repository) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(Guid id, NewMortgageTermRequest request)
    {
        await mediator.Send(new NewMortgageTermCommand(id, request.StartDate, request.LengthInMonths, request.InterestRate));
        return Ok();
    }

    // TODO: Add GET method to get all terms for a mortgage
    [HttpGet]
    public async Task<IActionResult> Get(Guid id)
    {
        return Ok(await repository.GetTerms(id));
    }
}

public class NewMortgageTermRequest
{
    public DateTime StartDate { get; set; }
    public int LengthInMonths { get; set; }
    public decimal InterestRate { get; set; }
}