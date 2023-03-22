using TaskManagerIO.API.ValueObjects;

namespace TaskManagerIO.API.Entities;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Lastname { get; set; }
    public string Fullname => Name + " " + Lastname;
    public string Username { get; set; }
    public UserRole UserRole { get; set; }
    public string Salt { get; set; }
    public string Password { get; set; }
    public int? DepartmentId { get; set; }
    public Department Department { get; set; }
    public List<Job> Jobs { get; set; } = new();
    public string RefreshToken { get; set; }
    public DateTime? RefreshTokenEndsAt { get; set; }
}
