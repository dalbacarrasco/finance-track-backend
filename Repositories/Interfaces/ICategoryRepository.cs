using FinTrackAPI.Models;

namespace FinTrackAPI.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllByUsuarioAsync(int usuarioId);
        Task<Category?> GetByIdAsync(int id, int usuarioId);
        Task<Category> CreateAsync(Category categoria);
        Task<Category> UpdateAsync(Category categoria);
        Task<bool> DeleteAsync(int id, int usuarioId);
    }
}