using Application.ServicesLayer.InterfazLayer;
using Application.Validations;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RankingController : ControllerBase
    {
        private readonly IArchivoRankingService _archivoRankingService;

        public RankingController(IArchivoRankingService archivoRankingService)
        {
            _archivoRankingService = archivoRankingService;
        }

        [HttpGet("generar-ranking-csv")]
        public async Task<IActionResult> GenerarRankingCsv([FromQuery] int? top)
        {
            var validator = new TopRankingValidator();
            var validationResult = await validator.ValidateAsync(top);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            var filePath = await _archivoRankingService.GenerarRankingCsvAsync(top);
            var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return File(fileBytes, "text/csv", Path.GetFileName(filePath));
        }
    }
}
