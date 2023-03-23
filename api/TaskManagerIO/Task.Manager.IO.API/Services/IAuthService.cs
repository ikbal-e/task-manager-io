using OneOf;
using TaskManagerIO.API.Common.Models;
using TaskManagerIO.API.DTOs;
using TaskManagerIO.API.ResourceParameters;

namespace TaskManagerIO.API.Services;

public interface IAuthService
{
    Task<OneOf<LoginResponseDto, NotFoundError>> LoginAsync(LoginParameters loginParameters);
    Task RegisterAsync(RegisterParameters registerParameters);
    Task<OneOf<LoginResponseDto, NotFoundError>> RefreshTokenAsync(string refreshToken, string expiredAccessToken);
    Task<OneOf<OperationSucceeded, NotFoundError>> ChangePassword(int userId, string oldPassword, string newPassword);
}

