using Domain.EntitiesLayer;
using Infrastructure.Persistence;
using Infrastructure.RepositoriesLayer.InterfazLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.RepositoriesLayer
{
    public class ArchivoRankingRepository: IArchivoRankingRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMemoryCache _cache;

        public ArchivoRankingRepository(ApplicationDbContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }


        public async Task<List<VideoJuego>> ObtenerRankingVideojuegosAsync()
        {
            return await _context.Videojuegos
                .FromSqlRaw("EXEC ObtenerRankingVideojuegos") 
                .Select(v => new VideoJuego
                {
                    Titulo = v.Titulo,
                    Compania = v.Compania,
                    PuntajePromedio = v.PuntajePromedio
                }).ToListAsync();
        }
    }
}
