using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagerIO.API.ResourceParameters;
using TaskManagerIO.API.Services;

namespace TaskManagerIO.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RegisterController : ControllerBase
{
    private readonly IAuthService _authService;

    public RegisterController(IAuthService authService)
    {
        _authService = authService;
    }

    [AllowAnonymous]
    [HttpPost()]
    public async Task<ActionResult<string>> Register([FromBody] RegisterParameters registerParameters)
    {
        await _authService.RegisterAsync(registerParameters);
        return Ok();
    }
}
