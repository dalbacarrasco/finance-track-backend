using System.Security.Claims;
using FinTrackAPI.DTOs;
using FinTrackAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinTrackAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoriaService;

        public CategoryController(ICategoryService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        private int GetUsuarioId() =>
            int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categorias = await _categoriaService.GetAllAsync(GetUsuarioId());
            return Ok(categorias);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var categoria = await _categoriaService.GetByIdAsync(id, GetUsuarioId());
            if (categoria == null) return NotFound();
            return Ok(categoria);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryRequestDTO dto)
        {
            var categoria = await _categoriaService.CreateAsync(dto, GetUsuarioId());
            return CreatedAtAction(nameof(GetById), new { id = categoria.Id }, categoria);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CategoryRequestDTO dto)
        {
            var categoria = await _categoriaService.UpdateAsync(id, dto, GetUsuarioId());
            if (categoria == null) return NotFound();
            return Ok(categoria);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _categoriaService.DeleteAsync(id, GetUsuarioId());
            if (!result) return NotFound();
            return NoContent();
        }
    }
}