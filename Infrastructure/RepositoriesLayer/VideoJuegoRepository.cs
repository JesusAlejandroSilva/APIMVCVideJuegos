using Domain.EntitiesLayer;
using Infrastructure.Persistence;
using Infrastructure.RepositoriesLayer.InterfazLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.RepositoriesLayer
{
    public class VideoJuegoRepository: IVideoJuegoRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMemoryCache _cache;

        public VideoJuegoRepository(ApplicationDbContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<IEnumerable<VideoJuego>> GetAllAsync()
        {
          return await _cache.GetOrCreateAsync("Videojuegos", async entry =>
          {
              entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
              return await _context.Videojuegos.FromSqlRaw("EXEC sp_GetAllVideojuegos").ToListAsync();
          });
        }

        public async Task<PaginatedResult<VideoJuego>> GetPaginatedAsync(int page, int pageSize, string nombre, string compania, int? anoLanzamiento)
        {
            var result = await _context.Videojuegos.FromSqlInterpolated($"EXEC sp_GetPaginatedVideojuegos {page}, {pageSize}, {nombre}, {compania}, {anoLanzamiento}")
                .ToListAsync();

            // Crear un objeto PaginatedResult personalizado para devolver resultados con paginación.
            var totalRecords = result.Count; // Esto debería ajustarse según cómo manejes el SP
            return new PaginatedResult<VideoJuego>(result, totalRecords, page, pageSize);
        }

        public async Task<VideoJuego> GetByIdAsync(int id)
        {
            return await _context.Videojuegos.FromSqlInterpolated($"EXEC sp_GetVideojuegoById {id}").FirstOrDefaultAsync();
        }

        public async Task CreateAsync(VideoJuego videojuego)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC sp_CreateVideojuego {videojuego.Titulo}, {videojuego.Compania}, {videojuego.AnioLanzamiento}, {videojuego.Precio}, {videojuego.PuntajePromedio}");
        }

        public async Task UpdateAsync(VideoJuego videojuego)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC sp_UpdateVideojuego {videojuego.Id}, {videojuego.Titulo}, {videojuego.Compania}, {videojuego.AnioLanzamiento}, {videojuego.Precio}, {videojuego.PuntajePromedio}");
        }

        public async Task DeleteAsync(int id)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC sp_DeleteVideojuego {id}");
        }
    }
}
