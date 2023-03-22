using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OneOf;
using TaskManagerIO.API.Common.Models;
using TaskManagerIO.API.DTOs;
using TaskManagerIO.API.ResourceParameters;
using TaskManagerIO.API.Services;

namespace TaskManagerIO.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly IAuthService _authService;

    public LoginController(IAuthService authService)
    {
        _authService = authService;
    }

    [AllowAnonymous]
    [HttpPost()]
    public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginParameters loginParameters)
        => (await _authService.LoginAsync(loginParameters))
            .Match<ActionResult<LoginResponseDto>>(
                loginResponseDto => Ok(loginResponseDto),
                notFoundError => Problem(notFoundError.Message, HttpContext.Request.Path, 404, "title")
            );
}