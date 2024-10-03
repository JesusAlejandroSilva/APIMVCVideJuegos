namespace Application.ServicesLayer.InterfazLayer
{
    public interface IArchivoRankingService
    {
        Task<string> GenerarRankingCsvAsync(int? top);
    }
}
