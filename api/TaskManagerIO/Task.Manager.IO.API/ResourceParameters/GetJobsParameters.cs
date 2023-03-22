using TaskManagerIO.API.ValueObjects;

namespace TaskManagerIO.API.ResourceParameters;

public class GetJobsParameters
{
    public int? Id { get; set; }
    public int? AssignedTo { get; set; }
    public JobStatus? Status { get; set; }
}