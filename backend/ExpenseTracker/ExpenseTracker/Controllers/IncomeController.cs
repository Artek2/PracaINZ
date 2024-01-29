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
  public class IncomeController : ControllerBase
  {
    private readonly IIncomeService _incomeService;
    public IncomeController(IIncomeService incomeService)
    {
      _incomeService = incomeService;
    }

    [HttpDelete("delete-income/{id}")]
    [SwaggerOperation(Summary = "Remove income")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> RemoveIncome([FromRoute] int id)
    {
      await _incomeService.DeleteAsync(id);

      return Ok(id);
    }

    [HttpPost("add-income")]
    [SwaggerOperation(Summary = "Add income")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> AddIncome(IncomeExpenseRequest request)
    {
      await _incomeService.CreateAsync(request);

      return Ok();
    }

    [HttpGet("get-income")]
    [SwaggerOperation(Summary = "Add income")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetIncome()
    {
      var result = await _incomeService.GetAllAsync();

      return Ok(result);
    }
  }
}
