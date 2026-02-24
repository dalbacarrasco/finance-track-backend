using FinTrackAPI.DTOs;

namespace FinTrackAPI.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryResponseDTO>> GetAllAsync(int usuarioId);
        Task<CategoryResponseDTO?> GetByIdAsync(int id, int usuarioId);
        Task<CategoryResponseDTO> CreateAsync(CategoryRequestDTO dto, int usuarioId);
        Task<CategoryResponseDTO?> UpdateAsync(int id, CategoryRequestDTO dto, int usuarioId);
        Task<bool> DeleteAsync(int id, int usuarioId);
    }
}