namespace TaskManagerIO.API.DTOs;

public class CreateJobResponseDto
{
    public int Id { get; set; }
    public string Description { get; set; }
    public string DepartmentName { get; set; }
    public string CreatedBy { get; set; }
}
