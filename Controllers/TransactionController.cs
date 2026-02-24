using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FinTrackAPI.DTOs;
using FinTrackAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinTrackAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transaccionService;

        public TransactionController(ITransactionService transaccionService)
        {
            _transaccionService = transaccionService;
        }

        private int GetUsuarioId() =>
            int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int mes, [FromQuery] int anio)
        {
            if (mes == 0) mes = DateTime.Now.Month;
            if (anio == 0) anio = DateTime.Now.Year;

            var transacciones = await _transaccionService.GetAllAsync(GetUsuarioId(), mes, anio);
            return Ok(transacciones);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var transaccion = await _transaccionService.GetByIdAsync(id, GetUsuarioId());
            if (transaccion == null) return NotFound();
            return Ok(transaccion);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TransactionRequestDTO dto)
        {
            var transaccion = await _transaccionService.CreateAsync(dto, GetUsuarioId());
            return CreatedAtAction(nameof(GetById), new { id = transaccion.Id }, transaccion);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TransactionRequestDTO dto)
        {
            var transaccion = await _transaccionService.UpdateAsync(id, dto, GetUsuarioId());
            if (transaccion == null) return NotFound();
            return Ok(transaccion);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _transaccionService.DeleteAsync(id, GetUsuarioId());
            if (!result) return NotFound();
            return NoContent();
        }
    }
}