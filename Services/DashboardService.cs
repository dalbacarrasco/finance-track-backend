using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinTrackAPI.Data;
using FinTrackAPI.DTOs;
using FinTrackAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinTrackAPI.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly AppDbContext _context;

        public DashboardService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResumeDTO> GetResumenAsync(int usuarioId, int mes, int año)
        {
            var transacciones = await _context.Transactions
                .Where(t => t.UserId == usuarioId
                    && t.Date.Month == mes
                    && t.Date.Year == año)
                .ToListAsync();

            var totalIngresos = transacciones
                .Where(t => t.Type == "ingreso")
                .Sum(t => t.Amount);

            var totalGastos = transacciones
                .Where(t => t.Type == "gasto")
                .Sum(t => t.Amount);

            return new ResumeDTO
            {
                TotalIncomings = totalIngresos,
                TotalExpenses = totalGastos,
                Balance = totalIngresos - totalGastos
            };
        }

        public async Task<IEnumerable<SpendingByCategoryDTO>> GetGastosPorCategoriaAsync(int usuarioId, int mes, int año)
        {
            return await _context.Transactions
                .Include(t => t.Category)
                .Where(t => t.UserId == usuarioId
                    && t.Type == "gasto"
                    && t.Date.Month == mes
                    && t.Date.Year == año)
                .GroupBy(t => t.Category)
                .Select(g => new SpendingByCategoryDTO
                {
                    CategoryName = g.Key.Name,
                    CategoryColor = g.Key.Color,
                    CategoryIcon = g.Key.Icon,
                    Total = g.Sum(t => t.Amount)
                })
                .ToListAsync();
        }
    }
}