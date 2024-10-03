using Application.ServicesLayer.InterfazLayer;
using Domain.EntitiesLayer;
using Infrastructure.RepositoriesLayer.InterfazLayer;

namespace Application.ServicesLayer
{
    public class VideoJuegoService: IVideoJuegoService
    {
        private readonly IVideoJuegoRepository _repository;

        public VideoJuegoService(IVideoJuegoRepository repository)
        {
            repository = _repository;
        }

        public async Task<IEnumerable<VideoJuego>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<PaginatedResult<VideoJuego>> GetPaginatedAsync(int page, int pageSize, string nombre, string compania, int? anoLanzamiento)
        {
            return await _repository.GetPaginatedAsync(page, pageSize, nombre, compania, anoLanzamiento);
        }

        public async Task<VideoJuego> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task CreateAsync(VideoJuego videojuego)
        {
            // Validaciones con FluentValidation antes de la creación
            await _repository.CreateAsync(videojuego);
        }

        public async Task UpdateAsync(VideoJuego videojuego)
        {
            // Validaciones con FluentValidation antes de la actualización
            await _repository.UpdateAsync(videojuego);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

    }
}
