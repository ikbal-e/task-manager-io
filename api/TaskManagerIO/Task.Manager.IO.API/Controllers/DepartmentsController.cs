using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using TaskManagerIO.API.DTOs;
using TaskManagerIO.API.Infrastructure.Filters;
using TaskManagerIO.API.Services;
using TaskManagerIO.API.ValueObjects;

namespace TaskManagerIO.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DepartmentsController : ControllerBase
{
    private readonly IDepartmentService _departmentService;

    public DepartmentsController(IDepartmentService departmentService)
    {
        _departmentService = departmentService;
    }

    [HttpGet(Name = nameof(GetDepartments))]
    public async Task<ActionResult<IEnumerable<DepartmentDto>>> GetDepartments()
    {
        var departments = await _departmentService.GetDepartmentsAsync();
        return Ok(departments);
    }

    [HttpGet("{id}", Name = nameof(GetDepartment))]
    public async Task<ActionResult<IEnumerable<DepartmentDto>>> GetDepartment([FromRoute] int id)
    {
        var department = await _departmentService.GetDepartmentAsync(id);

        if (department is null) return NotFound();

        return Ok(department);
    }


    [HttpPost(Name = nameof(CreateDepartment))]
    public async Task<ActionResult<DepartmentDto>> CreateDepartment(CreateDepartmentDto dto)
    {
        var department = await _departmentService.CreateDepartmentAsync(dto);
        return CreatedAtRoute(nameof(GetDepartment), new { id = department.Id }, department);
    }
}
