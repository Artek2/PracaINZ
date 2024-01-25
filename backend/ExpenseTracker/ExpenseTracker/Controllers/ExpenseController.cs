using Microsoft.AspNetCore.Authorization; // ASP.NET Core's Authorization framework
using Microsoft.AspNetCore.Mvc; // ASP.NET Core's MVC framework for building web APIs
using Services; // Custom service layer, typically a separate project within the solution
using Swashbuckle.AspNetCore.Annotations; // Swashbuckle for Swagger/OpenAPI annotation support
using System.Net.Mime; // .NET's System.Net.Mime for content type definitions

namespace ExpenseTracker.Controllers
{
    [ApiController] // ASP.NET Core MVC's annotation for automatic HTTP 400 responses
    [Route("[controller]")] // ASP.NET Core MVC's routing, "[controller]" gets replaced by the controller's name
    [Authorize] // ASP.NET Core's Authorization, requires authentication to access this controller
    public class ExpenseController : ControllerBase
    {
        // Dependency injection via constructor for IExpenseService which is part of the custom Services project
        private readonly IExpenseService _expenseService;
        public ExpenseController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        // HTTP DELETE method route configuration using ASP.NET Core MVC
        [HttpDelete("delete-expense/{id}")]
        [SwaggerOperation(Summary = "Remove expense")] // Swashbuckle Swagger for operation summary
        [Consumes(MediaTypeNames.Application.Json)] // Indicates expected media type for request body
        [ProducesResponseType(StatusCodes.Status200OK)] // HTTP response type, part of ASP.NET Core's MVC framework
        public async Task<IActionResult> Removeexpense([FromRoute] int id)
        {
            await _expenseService.DeleteAsync(id); // Asynchronous operation using C#'s Task-based programming model

            return Ok(id); // IActionResult with 200 OK status code, from ASP.NET Core MVC framework
        }

        // HTTP POST method route configuration using ASP.NET Core MVC
        [HttpPost("add-expense")]
        [SwaggerOperation(Summary = "Add expense")] // Swashbuckle Swagger for operation summary
        [Consumes(MediaTypeNames.Application.Json)] // Indicates expected media type for request body
        [ProducesResponseType(StatusCodes.Status200OK)] // HTTP response type, part of ASP.NET Core's MVC framework
        public async Task<IActionResult> Addexpense(IncomeExpenseRequest request)
        {
            await _expenseService.CreateAsync(request); // Asynchronous operation using C#'s Task-based programming model

            return Ok(); // IActionResult with 200 OK status code, from ASP.NET Core MVC framework
        }

        // HTTP GET method route configuration using ASP.NET Core MVC
        [HttpGet("get-expenses")]
        [SwaggerOperation(Summary = "Get expenses")] // Swashbuckle Swagger for operation summary
        [Consumes(MediaTypeNames.Application.Json)] // Indicates expected media type for request body
        [ProducesResponseType(StatusCodes.Status200OK)] // HTTP response type, part of ASP.NET Core's MVC framework
        public async Task<IActionResult> Getexpense()
        {
            var result = await _expenseService.GetAllAsync(); // Asynchronous operation using C#'s Task-based programming model

            return Ok(result); // IActionResult with 200 OK status code, from ASP.NET Core MVC framework
        }
    }
}
