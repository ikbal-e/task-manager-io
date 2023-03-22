using TaskManagerIO.API.DTOs;
using TaskManagerIO.API.ResourceParameters;

namespace TaskManagerIO.API.Services;

public interface IJobService
{
    Task<CreateJobResponseDto> CreateJobAsync(CreateJobRequestDto dto);
    Task<IEnumerable<JobDto>> GetJobs(GetJobsParameters parameters);
    Task AssignJobToMe(int jobId);
}
