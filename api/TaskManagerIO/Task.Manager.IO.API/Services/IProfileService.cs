using OneOf;
using TaskManagerIO.API.Common.Models;
using TaskManagerIO.API.DTOs;

namespace TaskManagerIO.API.Services;

public interface IProfileService
{
    Task<OneOf<ProfileInfoDto, NotFoundError>> GetProfileAsync(int userId);
    Task<List<ProfileInfoDto>> GetProfilesAsync();
}
