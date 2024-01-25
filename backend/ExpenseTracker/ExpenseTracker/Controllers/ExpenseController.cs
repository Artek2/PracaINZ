using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace ExpenseTracker.Controllers
{
  [ApiController]
  [Route("[controller]")]
  [Authorize]
  public class ExpenseController : ControllerBase
  {
    private readonly IExpenseService _expenseService;
    public ExpenseController(IExpenseService expenseService)
    {
      _expenseService = expenseService;
    }

    [HttpDelete("delete-expense/{id}")]
    [SwaggerOperation(Summary = "Remove expense")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Removeexpense([FromRoute] int id)
    {
      await _expenseService.DeleteAsync(id);

      return Ok(id);
    }

    [HttpPost("add-expense")]
    [SwaggerOperation(Summary = "Add expense")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Addexpense(IncomeExpenseRequest request)
    {
      await _expenseService.CreateAsync(request);

      return Ok();
    }

    [HttpGet("get-expenses")]
    [SwaggerOperation(Summary = "Add expense")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Getexpense()
    {
      var result = await _expenseService.GetAllAsync();

      return Ok(result);
    }
  }
}