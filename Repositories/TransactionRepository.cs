using FinTrackAPI.Data;
using FinTrackAPI.Models;
using FinTrackAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinTrackAPI.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _context;

        public TransactionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Transaction>> GetAllByUsuarioAsync(int usuarioId, int mes, int año)
        {
            return await _context.Transactions
                .Include(t => t.Category)
                .Where(t => t.UserId == usuarioId
                    && t.Date.Month == mes
                    && t.Date.Year == año)
                .OrderByDescending(t => t.Date)
                .ToListAsync();
        }

        public async Task<Transaction?> GetByIdAsync(int id, int usuarioId)
        {
            return await _context.Transactions
                .Include(t => t.Category)
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == usuarioId);
        }

        public async Task<Transaction> CreateAsync(Transaction transaccion)
        {
            _context.Transactions.Add(transaccion);
            await _context.SaveChangesAsync();
            return transaccion;
        }

        public async Task<Transaction> UpdateAsync(Transaction transaccion)
        {
            _context.Transactions.Update(transaccion);
            await _context.SaveChangesAsync();
            return transaccion;
        }

        public async Task<bool> DeleteAsync(int id, int usuarioId)
        {
            var transaccion = await GetByIdAsync(id, usuarioId);
            if (transaccion == null) return false;

            _context.Transactions.Remove(transaccion);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}