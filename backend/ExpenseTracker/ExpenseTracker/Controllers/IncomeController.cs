// Importing namespaces from various libraries/frameworks used in the controller
using Microsoft.AspNetCore.Authorization; // Part of the ASP.NET Core framework for enforcing authorization.
using Microsoft.AspNetCore.Mvc; // Core ASP.NET Core MVC framework for creating web APIs.
using Services; // Custom services presumably part of this solution for business logic.
using Swashbuckle.AspNetCore.Annotations; // Swashbuckle for Swagger/OpenAPI documentation annotations.
using System.Net.Mime; // .NET's System.Net.Mime for MIME type handling.

// Defining the namespace for this controller within the application's structure.
namespace ExpenseTracker.Controllers
{
    [ApiController] // Annotation from ASP.NET Core to denote this class as a controller with API-specific features.
    [Route("[controller]")] // ASP.NET Core routing, automates the use of the controller's name in the route.
    [Authorize] // ASP.NET Core's authorization filter, ensuring the controller's actions require a valid user.
    public class IncomeController : ControllerBase
    {
        // Field for the service dependency that handles income-related operations.
        private readonly IIncomeService _incomeService;

        // Constructor with dependency injection for the IIncomeService, coming from the custom Services layer.
        public IncomeController(IIncomeService incomeService)
        {
            _incomeService = incomeService; // Assignment of the injected service to the field.
        }

        // HTTP DELETE method to remove an income by its ID.
        [HttpDelete("delete-income/{id}")]
        [SwaggerOperation(Summary = "Remove income")] // Swashbuckle annotation for documenting this operation.
        [Consumes(MediaTypeNames.Application.Json)] // Specifies the expected request content type.
        [ProducesResponseType(StatusCodes.Status200OK)] // Indicates the HTTP status code returned on success.
        public async Task<IActionResult> RemoveIncome([FromRoute] int id)
        {
            await _incomeService.DeleteAsync(id); // Asynchronous call to delete the income record.

            return Ok(id); // Returns a 200 OK status with the ID of the deleted income.
        }

        // HTTP POST method to add a new income record.
        [HttpPost("add-income")]
        [SwaggerOperation(Summary = "Add income")] // Swashbuckle annotation for API documentation.
        [Consumes(MediaTypeNames.Application.Json)] // Indicates the method consumes JSON.
        [ProducesResponseType(StatusCodes.Status200OK)] // Expected success response code.
        public async Task<IActionResult> AddIncome(IncomeExpenseRequest request)
        {
            await _incomeService.CreateAsync(request); // Asynchronous call to create a new income record.

            return Ok(); // Returns a 200 OK status without content.
        }

        // HTTP GET method to retrieve all income records.
        [HttpGet("get-income")]
        [SwaggerOperation(Summary = "Get all income records")] // Corrected summary for retrieving income records.
        [Consumes(MediaTypeNames.Application.Json)] // Specifies the method consumes JSON.
        [ProducesResponseType(StatusCodes.Status200OK)] // Expected success response code.
        public async Task<IActionResult> GetIncome()
        {
            var result = await _incomeService.GetAllAsync(); // Asynchronous call to retrieve all income records.

            return Ok(result); // Returns a 200 OK status with the list of income records.
        }
    }
}
