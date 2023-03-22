namespace TaskManagerIO.API.DTOs;

public class LoginResponseDto
{
    public string Username { get; set; }
    public string AccessToken { get; set; }
    public DateTime ExpiresAt { get; set; }
    public string RefreshToken { get; set; }
}

