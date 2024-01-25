using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Model;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace ExpenseTracker.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class UserController : ControllerBase
  {
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
      _userService = userService;
    }

    [HttpPost("register")]
    [SwaggerOperation(Summary = "Register")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> RegisterUser([FromBody] RegisterUserDto dto)
    {
      await _userService.RegisterUserAsync(dto);

      return Ok("Utworzono użytkownika");
    }

    [HttpPost("login")]
    [SwaggerOperation(Summary = "Login")]
    public async Task<ActionResult<string>> Login([FromBody] LoginRequest dto)
    {
      (string token, string name) = await _userService.GenerateJwt(dto);

      return Ok(new { token, name });

    }
  }
}
