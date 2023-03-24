using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;
using TaskManagerIO.API.DTOs;
using TaskManagerIO.API.Services;

namespace TaskManagerIO.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProfilesController : ControllerBase
{
    private readonly IProfileService _profileService;
    private readonly IAuthService _authService;

    public ProfilesController(IProfileService profileService, IAuthService authService)
    {
        _profileService = profileService;
        _authService = authService;
    }

    [Authorize(Roles = "admin")]
    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<ProfileInfoDto>>> GetProfiles()
    {
        var profiles = await _profileService.GetProfilesAsync();
        
        return Ok(profiles);

    }

    [HttpGet("Me", Name = nameof(GetMyProfile))]
    public async Task<ActionResult<ProfileInfoDto>> GetMyProfile()
    {
        var userId = (User.Identity as ClaimsIdentity).FindFirst(x => x.Type == "id")?.Value;
        var profileResult = await _profileService.GetProfileAsync(int.Parse(userId));
        if (profileResult.TryPickT1(out var notFound, out var userInfo))
        {
            return NotFound(notFound.Message);
        }

        return Ok(userInfo);
    }

    [HttpPut("Me/ChangePassword")]
    public async Task<ActionResult> ChangePassword(ChangePasswordRequestDto requestDto)
    {
        var userId = (User.Identity as ClaimsIdentity).FindFirst(x => x.Type == "id")?.Value;
        var profileResult = await _authService.ChangePassword(int.Parse(userId), requestDto.OldPassword, requestDto.NewPassword);
        if (profileResult.TryPickT1(out var notFound, out _))
        {
            return NotFound(notFound.Message);
        }

        return Ok();
    }
}
