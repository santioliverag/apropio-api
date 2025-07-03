using Apropio.API.DTOs;
using Apropio.API.Models;

namespace Apropio.API.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto?> LoginAsync(LoginDto loginDto);
        Task<AuthResponseDto?> RegisterAsync(RegisterDto registerDto);
        Task<bool> ChangePasswordAsync(int userId, ChangePasswordDto changePasswordDto);
        Task<Usuario?> GetUserByIdAsync(int userId);
        Task<Usuario?> GetUserByEmailAsync(string email);
        Task<bool> ValidateTokenAsync(string token);
        string GenerateJwtToken(Usuario usuario);
    }
} 