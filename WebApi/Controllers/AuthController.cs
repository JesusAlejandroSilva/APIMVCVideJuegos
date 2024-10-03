using Application.ServicesLayer.InterfazLayer;
using Domain.EntitiesLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistroModel model)
        {
            var result = await _authService.RegisterUserAsync(model);
            if (result.Succeeded)
            {
                return Ok(new { Message = "Usuario registrado con éxito." });
            }
            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var token = await _authService.LoginUserAsync(model);
            if (token == null)
            {
                return Unauthorized(new { Message = "Credenciales incorrectas." });
            }
            return Ok(new { Token = token });
        }
    }
}
