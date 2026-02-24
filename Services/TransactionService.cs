using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinTrackAPI.DTOs;
using FinTrackAPI.Models;
using FinTrackAPI.Repositories.Interfaces;
using FinTrackAPI.Services.Interfaces;

namespace FinTrackAPI.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transaccionRepository;

        public TransactionService(ITransactionRepository transaccionRepository)
        {
            _transaccionRepository = transaccionRepository;
        }

        public async Task<IEnumerable<TransactionResponseDTO>> GetAllAsync(int usuarioId, int mes, int año)
        {
            var transacciones = await _transaccionRepository.GetAllByUsuarioAsync(usuarioId, mes, año);
            return transacciones.Select(t => ToDTO(t));
        }

        public async Task<TransactionResponseDTO?> GetByIdAsync(int id, int usuarioId)
        {
            var transaccion = await _transaccionRepository.GetByIdAsync(id, usuarioId);
            return transaccion == null ? null : ToDTO(transaccion);
        }

        public async Task<TransactionResponseDTO> CreateAsync(TransactionRequestDTO dto, int usuarioId)
        {
            var transaccion = new Transaction
            {
                Amount = dto.Amount,
                Description = dto.Description,
                Date = DateTime.SpecifyKind(dto.Date, DateTimeKind.Utc),
                Type = dto.Type,
                CategoryId = dto.CategoryId,
                UserId = usuarioId
            };

            await _transaccionRepository.CreateAsync(transaccion);

            var creada = await _transaccionRepository.GetByIdAsync(transaccion.Id, usuarioId);
            return ToDTO(creada!);
        }

        public async Task<TransactionResponseDTO?> UpdateAsync(int id, TransactionRequestDTO dto, int usuarioId)
        {
            var transaccion = await _transaccionRepository.GetByIdAsync(id, usuarioId);
            if (transaccion == null) return null;

            transaccion.Amount = dto.Amount;
            transaccion.Description = dto.Description;
            transaccion.Date = DateTime.SpecifyKind(dto.Date, DateTimeKind.Utc);
            transaccion.Type = dto.Type;
            transaccion.CategoryId = dto.CategoryId;

            await _transaccionRepository.UpdateAsync(transaccion);
            return ToDTO(transaccion);
        }

        public async Task<bool> DeleteAsync(int id, int usuarioId)
        {
            return await _transaccionRepository.DeleteAsync(id, usuarioId);
        }

        private TransactionResponseDTO ToDTO(Transaction t) => new()
        {
            Id = t.Id,
            Amount = t.Amount,
            Description = t.Description,
            Date = t.Date,
            Type = t.Type,
            CategoryId = t.CategoryId,
            CategoryName = t.Category.Name,
            CategoryColor = t.Category.Color,
            CategoryIcon = t.Category.Icon
        };
    }
}