using MediatR;
using Microsoft.AspNetCore.Mvc;
using Morgly.Application.Features.Mortgage.Command;
using Morgly.Application.Interfaces;

namespace Morgly.API.Controllers;

[ApiController]
[Route("[controller]")]
public class MortgagesController(IMediator mediator, IMortgageRepository mortgageRepository) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index() => Ok(await mortgageRepository.GetAll());


    [HttpPost]
    public async Task<IActionResult> Create(NewMortgageRequest request)
    {
        //var mul = (int y) => y * y;
        //mul(20);

        try
        {
            var id = await mediator.Send(new CreateMortgageCommand("23232", 1.5m, request.Amount, 3));
            return Created("/Mortgages", id);
        }
        catch (Exception)
        {

            throw;
        }
    }
}

public class NewMortgageRequest
{
    public decimal Amount { get; set; }
}