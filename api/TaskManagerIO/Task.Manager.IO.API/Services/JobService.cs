using Microsoft.EntityFrameworkCore;
using TaskManagerIO.API.DTOs;
using TaskManagerIO.API.Entities;
using TaskManagerIO.API.Infrastructure.Data;
using TaskManagerIO.API.ResourceParameters;

namespace TaskManagerIO.API.Services;

public class JobService : IJobService
{
    private readonly TaskManagerContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public JobService(TaskManagerContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<IEnumerable<JobDto>> GetJobs(GetJobsParameters parameters)
    {
        var jobs = _context.Jobs.AsQueryable();

        if (parameters.Id is not null) jobs.Where(x => x.Id == parameters.Id);
        if (parameters.AssignedTo is not null) jobs.Where(x => x.AssignedToId == parameters.AssignedTo);
        if (parameters.Status is not null) jobs.Where(x => x.Status == parameters.Status);

        var jobsDto = await jobs.Select(x => new JobDto
        {
            Id = x.Id,
            AssignedTo = x.AssignedTo.Fullname,
            AssignedToId = x.AssignedToId,
            DepartmentId = x.DepartmnetId,
            DepartmentName = x.Department.Name,
            Description = x.Description,
            Status = x.Status
        }).ToListAsync();

        return jobsDto;
    }

    public async Task<CreateJobResponseDto> CreateJobAsync(CreateJobRequestDto dto)
    {
        //var identity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;

        var job = new Job
        {
            CreatedById = 1, //TODO:
            Description = dto.Description,
            DepartmnetId = dto.DepartmentId,
        };

        _context.Jobs.Add(job);
        await _context.SaveChangesAsync();

        job = await _context.Jobs
            .Include(x => x.CreatedBy)
            .Include(x => x.Department)
            .Where(x => x.Id == job.Id)
            .FirstOrDefaultAsync();

        return new()
        {
            Id = job.Id,
            CreatedBy = job.CreatedBy.Fullname,
            Description = job.Description,
            DepartmentName = job.Department.Name,
        };
    }

    public async Task AssignJobToMe(int jobId)
    {
        var job = await _context.Jobs.Where(x => x.Id == jobId).FirstOrDefaultAsync();
        if (job is null) throw new ArgumentException();

        //TODO: other validations
        // Get CurrentUser's Id
        //job.AssignedTo = 

    }
}