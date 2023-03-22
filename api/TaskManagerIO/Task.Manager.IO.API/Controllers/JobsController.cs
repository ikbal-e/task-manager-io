using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;
using TaskManagerIO.API.DTOs;
using TaskManagerIO.API.ResourceParameters;
using TaskManagerIO.API.Services;

namespace TaskManagerIO.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class JobsController : ControllerBase
{
    private readonly IJobService _jobService;

    public JobsController(IJobService jobService)
    {
        _jobService = jobService;
    }

    [HttpGet(Name = nameof(GetJobs))]
    public async Task<ActionResult<IEnumerable<JobDto>>> GetJobs([FromQuery] GetJobsParameters parameters)
    {
        var jobs = await _jobService.GetJobs(parameters);

        return Ok(jobs);
    }

    [HttpGet("{id}", Name = nameof(GetJob))]
    public async Task<ActionResult<JobDto>> GetJob([FromRoute] int id)
    {
        var job = await _jobService.GetJobs(new() { Id = id });
        if (!job.Any())
        {
            return NotFound();
        }

        return Ok(job);
    }

    //[AuthorizedFor(UserRole.Admin)] // TODO: 
    //[Authorize(Roles = "admin")]
    [HttpPost(Name = nameof(CreateJob))]
    public async Task<ActionResult<CreateJobResponseDto>> CreateJob([FromBody] CreateJobRequestDto dto)
    {
        //TODO:
        var id1 = User.Identity as ClaimsIdentity;

        var id4 = id1.FindFirst("id")?.Value;
        var currentUserId = id1.FindFirst("subject")?.Value;

        var job = await _jobService.CreateJobAsync(dto);

        return CreatedAtRoute(nameof(GetJob), new { id = job.Id }, job);
    }

    [HttpPut("{id}/assign/me", Name = nameof(AssignJobToMe))]
    public async Task<ActionResult> AssignJobToMe(int id)
    {
        await _jobService.AssignJobToMe(id);

        return NoContent();
    }
}
