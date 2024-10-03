using Domain.EntitiesLayer;

namespace Infrastructure.RepositoriesLayer.InterfazLayer
{
    public interface IVideoJuegoRepository
    {
        Task<IEnumerable<VideoJuego>> GetAllAsync();
        Task<PaginatedResult<VideoJuego>> GetPaginatedAsync(int page, int pageSize, string nombre, string compania, int? anoLanzamiento);
        Task<VideoJuego> GetByIdAsync(int id);
        Task CreateAsync(VideoJuego videojuego);
        Task UpdateAsync(VideoJuego videojuego);
        Task DeleteAsync(int id);
    }
}
