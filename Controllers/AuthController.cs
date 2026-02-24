using FinTrackAPI.DTOs;
using FinTrackAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinTrackAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            var result = await _authService.RegisterAsync(dto);

            if (result == null)
                return BadRequest(new { message = "El email ya está registrado" });

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            var result = await _authService.LoginAsync(dto);

            if (result == null)
                return Unauthorized(new { message = "Email o contraseña incorrectos" });

            return Ok(result);
        }
    }
}