namespace TaskManagerIO.API.Entities;

public class Department
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<User> Employees { get; set; } = new();
}
