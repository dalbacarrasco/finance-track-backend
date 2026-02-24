using FinTrackAPI.Models;

namespace FinTrackAPI.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task<bool> ExistsEmailAsync(string email);
        Task<User> CreateAsync(User usuario);
    }
}