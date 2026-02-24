using FinTrackAPI.Data;
using FinTrackAPI.Models;
using FinTrackAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinTrackAPI.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllByUsuarioAsync(int usuarioId)
        {
            return await _context.Categories
                .Where(c => c.UserId == usuarioId)
                .ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int id, int usuarioId)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == id && c.UserId == usuarioId);
        }

        public async Task<Category> CreateAsync(Category categoria)
        {
            _context.Categories.Add(categoria);
            await _context.SaveChangesAsync();
            return categoria;
        }

        public async Task<Category> UpdateAsync(Category categoria)
        {
            _context.Categories.Update(categoria);
            await _context.SaveChangesAsync();
            return categoria;
        }

        public async Task<bool> DeleteAsync(int id, int usuarioId)
        {
            var categoria = await GetByIdAsync(id, usuarioId);
            if (categoria == null) return false;

            _context.Categories.Remove(categoria);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}