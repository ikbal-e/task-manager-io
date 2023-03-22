using TaskManagerIO.API.ValueObjects;

namespace TaskManagerIO.API.DTOs;

public class JobDto
{
    public int Id { get; set; }
    public string Description { get; set; }
    public string DepartmentName { get; set; }
    public JobStatus Status { get; set; }
    public int DepartmentId { get; set; }
    public string AssignedTo { get; set; }
    public int? AssignedToId { get; set; }
}
