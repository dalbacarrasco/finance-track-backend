using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinTrackAPI.Models;

namespace FinTrackAPI.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetAllByUsuarioAsync(int usuarioId, int mes, int anio);
        Task<Transaction?> GetByIdAsync(int id, int usuarioId);
        Task<Transaction> CreateAsync(Transaction transaccion);
        Task<Transaction> UpdateAsync(Transaction transaccion);
        Task<bool> DeleteAsync(int id, int usuarioId);
    }
}