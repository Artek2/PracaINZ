using Microsoft.AspNetCore.Mvc; // ASP.NET Core MVC framework for building web APIs.
using Services; // Custom service layer, part of this solution.
using Services.Model; // Model classes provided by the Services layer.
using Swashbuckle.AspNetCore.Annotations; // Swashbuckle for Swagger/OpenAPI documentation annotations.
using System.Net.Mime; // .NET's System.Net.Mime for content type definitions.

namespace ExpenseTracker.Controllers
{
    [ApiController] // Annotation from ASP.NET Core to indicate this class as a controller with API-specific features.
    [Route("[controller]")] // ASP.NET Core routing, the "[controller]" token is replaced by the controller's name.
    public class UserController : ControllerBase
    {
        // Private field for the user service dependency.
        private readonly IUserService _userService;

        // Constructor with dependency injection for the IUserService, part of the Services layer.
        public UserController(IUserService userService)
        {
            _userService = userService; // Injected service is assigned to the private field.
        }

        // Endpoint for user registration.
        [HttpPost("register")] // HTTP POST method indicating this action responds to a POST request at "/user/register".
        [SwaggerOperation(Summary = "Register")] // Swashbuckle annotation for documenting this operation in Swagger UI.
        [Consumes(MediaTypeNames.Application.Json)] // Specifies the expected content type of the request body.
        [ProducesResponseType(StatusCodes.Status200OK)] // Indicates the HTTP status code returned on success.
        public async Task<ActionResult> RegisterUser([FromBody] RegisterUserDto dto)
        {
            await _userService.RegisterUserAsync(dto); // Asynchronous operation to register the user.

            return Ok("Utworzono użytkownika"); // Returns a 200 OK status with a custom message.
        }

        // Endpoint for user login.
        [HttpPost("login")] // HTTP POST method indicating this action responds to a POST request at "/user/login".
        [SwaggerOperation(Summary = "Login")] // Swashbuckle annotation for documenting this operation.
        public async Task<ActionResult<string>> Login([FromBody] LoginRequest dto)
        {
            (string token, string name) = await _userService.GenerateJwt(dto); // Asynchronous operation to log in the user and generate a JWT.

            return Ok(new { token, name }); // Returns a 200 OK status with the JWT token and user's name.
        }
    }
}
