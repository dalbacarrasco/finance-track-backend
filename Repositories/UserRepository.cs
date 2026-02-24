using FinTrackAPI.Data;
using FinTrackAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FinTrackAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> ExistsEmailAsync(string email)
        {
            return await _context.Users
                .AnyAsync(u => u.Email == email);
        }

        public async Task<User> CreateAsync(User usuario)
        {
            _context.Users.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }
    }
}