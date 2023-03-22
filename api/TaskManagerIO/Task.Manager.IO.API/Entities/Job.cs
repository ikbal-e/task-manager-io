using TaskManagerIO.API.ValueObjects;

namespace TaskManagerIO.API.Entities;

public class Job
{
    public int Id { get; set; }
    public string Description { get; set; }
    public int DepartmnetId { get; set; }
    public Department Department { get; set; }
    public JobStatus Status { get; set; }
    public int CreatedById { get; set; }
    public User CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int? AssignedToId { get; set; }
    public User AssignedTo { get; set; }
    public int? ApprovedById { get; set; }
    public User ApprovedBy { get; set; }
    public DateTime? ApprovedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public int? ClosedById { get; set; }
    public User ClosedBy { get; set; }
}
