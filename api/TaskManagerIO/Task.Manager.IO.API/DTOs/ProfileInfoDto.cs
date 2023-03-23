using TaskManagerIO.API.Entities;
using TaskManagerIO.API.ValueObjects;

namespace TaskManagerIO.API.DTOs;

public class ProfileInfoDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Lastname { get; set; }
    public string Fullname => Name + " " + Lastname;
    public string Username { get; set; }
    public UserRole UserRole { get; set; }
    public int? DepartmentId { get; set; }
    public string DepartmentName { get; set; }
}
