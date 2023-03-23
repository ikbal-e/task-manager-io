using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using OneOf;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
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

    [AllowAnonymous]
    [HttpPost("Refresh")]
    public async Task<ActionResult<LoginResponseDto>> Refresh([FromBody] string refreshToken)
    {
        HttpContext.Request.Headers.TryGetValue(HeaderNames.Authorization, out var accessToken);
        accessToken = accessToken.FirstOrDefault()?.Replace("Bearer ", string.Empty);
        if (string.IsNullOrEmpty(accessToken))
        {
            return NotFound();
        }

        return (await _authService.RefreshTokenAsync(refreshToken, accessToken))
                    .Match<ActionResult<LoginResponseDto>>(
                        loginResponseDto => Ok(loginResponseDto),
                        notFoundError => Problem(notFoundError.Message, HttpContext.Request.Path, 404, "title")
                        );
    }
}