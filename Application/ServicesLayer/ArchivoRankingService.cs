using Application.ServicesLayer.InterfazLayer;
using Infrastructure.RepositoriesLayer.InterfazLayer;
using System.Text;

namespace Application.ServicesLayer
{
    public class ArchivoRankingService : IArchivoRankingService
    {
        private readonly IArchivoRankingRepository _archivo;
        public ArchivoRankingService(IArchivoRankingRepository archivo)
        {
            _archivo = archivo;
        }
        public async Task<string> GenerarRankingCsvAsync(int? top)
        {
            // Obtener los videojuegos con su puntaje promedio
            var videojuegos = await _archivo.ObtenerRankingVideojuegosAsync();

            // Aplicar filtro de Top
            int topValue = top.HasValue && top.Value > 0 ? top.Value : 20;

            // Ordenar por puntaje descendente
            var ranking = videojuegos
                .OrderByDescending(v => v.PuntajePromedio)
                .Take(topValue)
                .ToList();

            // Clasificar los videojuegos (GOTY o AAA)
            int mitadTop = topValue / 2;
            for (int i = 0; i < ranking.Count; i++)
            {
                ranking[i].Calificacion = i < mitadTop ? "GOTY" : "AAA";
            }

            // Generar CSV
            var sb = new StringBuilder();
            sb.AppendLine("Nombre|Compañía|Puntaje|Clasificación");

            foreach (var videojuego in ranking)
            {
                sb.AppendLine($"{videojuego.Titulo}|{videojuego.Compania}|{videojuego.PuntajePromedio:F2}|{videojuego.Calificaciones}");
            }

            // Guardar archivo temporalmente
            var fileName = $"Ranking_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
            var filePath = Path.Combine(Path.GetTempPath(), fileName);
            await File.WriteAllTextAsync(filePath, sb.ToString());

            return filePath;
        }


    }
}
