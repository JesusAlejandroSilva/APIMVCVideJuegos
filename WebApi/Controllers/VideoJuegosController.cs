using Application.ServicesLayer;
using Application.ServicesLayer.InterfazLayer;
using Application.Validations;
using Domain.EntitiesLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoJuegosController : ControllerBase
    {
        private readonly IVideoJuegoService _service;

        public VideoJuegosController(IVideoJuegoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var videojuegos = await _service.GetAllAsync();
            return Ok(videojuegos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var videojuego = await _service.GetByIdAsync(id);
            if (videojuego == null)
            {
                return NotFound();
            }
            return Ok(videojuego);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] VideoJuego videojuego)
        {
            var validator = new VideojuegoValidator();
            var validationResult = await validator.ValidateAsync(videojuego);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            await _service.CreateAsync(videojuego);
            return CreatedAtAction(nameof(GetById), new { id = videojuego.Id }, videojuego);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] VideoJuego videojuego)
        {
            if (id != videojuego.Id)
            {
                return BadRequest();
            }

            var validator = new VideojuegoValidator();
            var validationResult = await validator.ValidateAsync(videojuego);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            await _service.UpdateAsync(videojuego);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }

    }
}
