using FinTrackAPI.DTOs;

namespace FinTrackAPI.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<IEnumerable<TransactionResponseDTO>> GetAllAsync(int usuarioId, int mes, int año);
        Task<TransactionResponseDTO?> GetByIdAsync(int id, int usuarioId);
        Task<TransactionResponseDTO> CreateAsync(TransactionRequestDTO dto, int usuarioId);
        Task<TransactionResponseDTO?> UpdateAsync(int id, TransactionRequestDTO dto, int usuarioId);
        Task<bool> DeleteAsync(int id, int usuarioId);
    }
}