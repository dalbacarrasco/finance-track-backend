using System.Security.Claims;
using FinTrackAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinTrackAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        private int GetUsuarioId() =>
            int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        [HttpGet("resume")]
        public async Task<IActionResult> GetResumen([FromQuery] int mes, [FromQuery] int año)
        {
            if (mes == 0) mes = DateTime.Now.Month;
            if (año == 0) año = DateTime.Now.Year;

            var resumen = await _dashboardService.GetResumenAsync(GetUsuarioId(), mes, año);
            return Ok(resumen);
        }

        [HttpGet("by-category")]
        public async Task<IActionResult> GetPorCategoria([FromQuery] int mes, [FromQuery] int año)
        {
            if (mes == 0) mes = DateTime.Now.Month;
            if (año == 0) año = DateTime.Now.Year;

            var gastos = await _dashboardService.GetGastosPorCategoriaAsync(GetUsuarioId(), mes, año);
            return Ok(gastos);
        }
    }
}