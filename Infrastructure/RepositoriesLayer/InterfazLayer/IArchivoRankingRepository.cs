using Domain.EntitiesLayer;

namespace Infrastructure.RepositoriesLayer.InterfazLayer
{
    public interface IArchivoRankingRepository
    {
        Task<List<VideoJuego>> ObtenerRankingVideojuegosAsync();
    }
}
