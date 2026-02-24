using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FinTrackAPI.DTOs;
using FinTrackAPI.Models;
using FinTrackAPI.Repositories;
using FinTrackAPI.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace FinTrackAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _usuarioRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository usuarioRepository, IConfiguration configuration)
        {
            _usuarioRepository = usuarioRepository;
            _configuration = configuration;
        }

        public async Task<AuthResponseDTO?> RegisterAsync(RegisterDTO dto)
        {
            if (await _usuarioRepository.ExistsEmailAsync(dto.Email))
                return null;

            var usuario = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            await _usuarioRepository.CreateAsync(usuario);

            return new AuthResponseDTO
            {
                Token = GenerateToken(usuario),
                Name = usuario.Name,
                Email = usuario.Email
            };
        }

        public async Task<AuthResponseDTO?> LoginAsync(LoginDTO dto)
        {
            var usuario = await _usuarioRepository.GetByEmailAsync(dto.Email);

            if (usuario == null || !BCrypt.Net.BCrypt.Verify(dto.Password, usuario.PasswordHash))
                return null;

            return new AuthResponseDTO
            {
                Token = GenerateToken(usuario),
                Name = usuario.Name,
                Email = usuario.Email
            };
        }

        private string GenerateToken(User usuario)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Email, usuario.Email),
            new Claim(ClaimTypes.Name, usuario.Name)
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}