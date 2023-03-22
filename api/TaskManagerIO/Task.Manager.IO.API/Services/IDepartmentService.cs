using TaskManagerIO.API.DTOs;

namespace TaskManagerIO.API.Services;

public interface IDepartmentService
{
    Task<IEnumerable<DepartmentDto>> GetDepartmentsAsync();
    Task<DepartmentDto> GetDepartmentAsync(int id);
    Task<DepartmentDto> CreateDepartmentAsync(CreateDepartmentDto dto);
}
