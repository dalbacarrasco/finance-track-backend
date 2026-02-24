using FinTrackAPI.DTOs;
using FinTrackAPI.Models;
using FinTrackAPI.Repositories.Interfaces;
using FinTrackAPI.Services.Interfaces;

namespace FinTrackAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoriaRepository;

        public CategoryService(ICategoryRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task<IEnumerable<CategoryResponseDTO>> GetAllAsync(int usuarioId)
        {
            var categorias = await _categoriaRepository.GetAllByUsuarioAsync(usuarioId);
            return categorias.Select(c => ToDTO(c));
        }

        public async Task<CategoryResponseDTO?> GetByIdAsync(int id, int usuarioId)
        {
            var categoria = await _categoriaRepository.GetByIdAsync(id, usuarioId);
            return categoria == null ? null : ToDTO(categoria);
        }

        public async Task<CategoryResponseDTO> CreateAsync(CategoryRequestDTO dto, int usuarioId)
        {
            var categoria = new Category
            {
                Name = dto.Name,
                Type = dto.Type,
                Color = dto.Color,
                Icon = dto.Icon,
                UserId = usuarioId
            };

            await _categoriaRepository.CreateAsync(categoria);
            return ToDTO(categoria);
        }

        public async Task<CategoryResponseDTO?> UpdateAsync(int id, CategoryRequestDTO dto, int usuarioId)
        {
            var categoria = await _categoriaRepository.GetByIdAsync(id, usuarioId);
            if (categoria == null) return null;

            categoria.Name = dto.Name;
            categoria.Type = dto.Type;
            categoria.Color = dto.Color;
            categoria.Icon = dto.Icon;

            await _categoriaRepository.UpdateAsync(categoria);
            return ToDTO(categoria);
        }

        public async Task<bool> DeleteAsync(int id, int usuarioId)
        {
            return await _categoriaRepository.DeleteAsync(id, usuarioId);
        }

        private CategoryResponseDTO ToDTO(Category categoria) => new()
        {
            Id = categoria.Id,
            Name = categoria.Name,
            Type = categoria.Type,
            Color = categoria.Color,
            Icon = categoria.Icon
        };
    }
}