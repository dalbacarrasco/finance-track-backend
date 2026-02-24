using FinTrackAPI.DTOs;

namespace FinTrackAPI.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDTO?> RegisterAsync(RegisterDTO dto);
        Task<AuthResponseDTO?> LoginAsync(LoginDTO dto);
    }
}