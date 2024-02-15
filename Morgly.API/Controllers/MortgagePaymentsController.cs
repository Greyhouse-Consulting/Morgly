using Microsoft.AspNetCore.Mvc;
using Morgly.Application.Interfaces;

namespace Morgly.API.Controllers;

[ApiController]
[Route("mortgage/{id}/payments")]

public class MortgagePaymentsController(IMortgageRepository repository) : Controller
{
    // GET
    [HttpGet]
    public async Task<IActionResult> Get(Guid id)
    {
        var m = await repository.Get(id);

        return Ok(m.CalculateMonthlyPayments(2000, 12));
    }
}