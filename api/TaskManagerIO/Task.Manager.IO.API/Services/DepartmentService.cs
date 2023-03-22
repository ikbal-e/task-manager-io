using Microsoft.EntityFrameworkCore;
using TaskManagerIO.API.DTOs;
using TaskManagerIO.API.Entities;
using TaskManagerIO.API.Infrastructure.Data;

namespace TaskManagerIO.API.Services;

public class DepartmentService : IDepartmentService
{
    private readonly TaskManagerContext _context;

    public DepartmentService(TaskManagerContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<DepartmentDto>> GetDepartmentsAsync()
    {
        var departments = await _context.Departments.Select(x => new DepartmentDto
        {
            Id = x.Id,
            Name = x.Name
        }).ToListAsync();

        return departments;
    }

    public async Task<DepartmentDto> GetDepartmentAsync(int id)
    {
        var department = await _context.Departments.Where(x => x.Id == id).Select(x => new DepartmentDto
        {
            Id = x.Id,
            Name = x.Name
        }).FirstOrDefaultAsync();

        return department;
    }

    public async Task<DepartmentDto> CreateDepartmentAsync(CreateDepartmentDto dto)
    {
        var isDepartmentExists = _context.Departments.Where(x => x.Name.Trim() == dto.Name.Trim()).Any();
        if (isDepartmentExists)
        {
            throw new ArgumentException("Department Exists");
        }

        var department = new Department
        {
            Name = dto.Name
        };

        _context.Departments.Add(department);
        await _context.SaveChangesAsync();

        return new()
        {
            Id = department.Id,
            Name = department.Name
        };
    }
}

