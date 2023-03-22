﻿using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OneOf;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TaskManagerIO.API.Common.Models;
using TaskManagerIO.API.DTOs;
using TaskManagerIO.API.Entities;
using TaskManagerIO.API.Infrastructure.Data;
using TaskManagerIO.API.ResourceParameters;

namespace TaskManagerIO.API.Services;

public class AuthService : IAuthService
{
    private readonly TaskManagerContext _context;
    private readonly IConfiguration _configuration;

    public AuthService(TaskManagerContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<OneOf<LoginResponseDto, NotFoundError>> LoginAsync(LoginParameters loginParameters)
    {
        var user = await _context.Users.Where(x => x.Username == loginParameters.Username).FirstOrDefaultAsync();
        if (user is null) return new NotFoundError("Not found");

        var salt = Convert.FromBase64String(user.Salt);
        string hashed = Convert.ToBase64String(ComputeteHash(loginParameters.Password, salt));

        if (hashed != user.Password) return new NotFoundError("Not Found");

        (string accessToken, DateTime expiresAt) = CreateAccessToken(user);
        var refreshToken = CreateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenEndsAt = DateTime.UtcNow.AddDays(1);

        return new LoginResponseDto()
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            Username = user.Username,
            ExpiresAt = expiresAt
        };
    }

    private (string token, DateTime expiresAt) CreateAccessToken(User user)
    {
        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("id", user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role,"admin"), // TODO: do not hardcode
                new Claim(ClaimTypes.Role,"user"),
             }),
            Expires = DateTime.UtcNow.AddMinutes(45),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var accessToken = tokenHandler.WriteToken(token);

        return (accessToken, tokenDescriptor.Expires.Value);
    }

    public async Task RegisterAsync(RegisterParameters registerParameters)
    {
        var isUserExists = await _context.Users.Where(x => x.Username == registerParameters.Username).AnyAsync();
        if (isUserExists)
        {
            throw new ArgumentException(); //TODO:
        }

        var saltBytes = RandomNumberGenerator.GetBytes(128 / 8);
        var saltBase64 = Convert.ToBase64String(saltBytes);
        string hashedPassword = Convert.ToBase64String(ComputeteHash(registerParameters.Password, saltBytes));

        var user = new User
        {
            Username = registerParameters.Username,
            Salt = saltBase64,
            Password = hashedPassword
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    private byte[] ComputeteHash(string text, byte[] salt) => 
        KeyDerivation.Pbkdf2(
            password: text,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8);

    private string CreateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var random = RandomNumberGenerator.Create();
        random.GetBytes(randomNumber);
        var refreshToken = Convert.ToBase64String(randomNumber);

        return refreshToken;
    }
}