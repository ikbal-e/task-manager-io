using Microsoft.EntityFrameworkCore;
using OneOf;
using TaskManagerIO.API.Common.Models;
using TaskManagerIO.API.DTOs;
using TaskManagerIO.API.Entities;
using TaskManagerIO.API.Infrastructure.Data;

namespace TaskManagerIO.API.Services;

public class ProfileService : IProfileService
{
    private readonly TaskManagerContext _context;

    public ProfileService(TaskManagerContext context)
    {
        _context = context;
    }

    public async Task<OneOf<ProfileInfoDto, NotFoundError>> GetProfileAsync(int userId)
    {
        var userInfo = await _context.Users.Where(x => x.Id == userId).Select(x => new ProfileInfoDto
        {
            Id = x.Id,
            Name = x.Name,
            Lastname = x.Lastname,
            DepartmentId = x.DepartmentId,
            DepartmentName = x.Department.Name,
            Username = x.Username,
            UserRole = x.UserRole
        }).FirstOrDefaultAsync();

        if (userInfo is null) return new NotFoundError("User not found");

        return userInfo;
    }

    public async Task<List<ProfileInfoDto>> GetProfilesAsync()
    {
        var userProfiles = await _context.Users.Select(x => new ProfileInfoDto
        {
            Id = x.Id,
            Name = x.Name,
            Lastname = x.Lastname,
            DepartmentId = x.DepartmentId,
            DepartmentName = x.Department.Name,
            Username = x.Username,
            UserRole = x.UserRole
        }).ToListAsync();

        return userProfiles;
    }
}
