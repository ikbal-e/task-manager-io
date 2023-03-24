using TaskManagerIO.API.ValueObjects;

namespace TaskManagerIO.API.ResourceParameters;

public class RegisterParameters
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public string Lastname { get; set; }
    public int? DepartmentId { get; set; }
    public UserRole? UserRole { get; set; }
}