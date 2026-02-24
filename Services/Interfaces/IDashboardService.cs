using FinTrackAPI.DTOs;

namespace FinTrackAPI.Services.Interfaces
{
    public interface IDashboardService
    {
        Task<ResumeDTO> GetResumenAsync(int usuarioId, int mes, int año);
        Task<IEnumerable<SpendingByCategoryDTO>> GetGastosPorCategoriaAsync(int usuarioId, int mes, int año);
    }
}