namespace Domain.EntitiesLayer
{
    public class VideoJuego
    {
        public int Id { get; set; } 
        public string Titulo { get; set; } 
        public string Compania { get; set; } 
        public int AnioLanzamiento { get; set; } 
        public decimal Precio { get; set; } 
        public decimal PuntajePromedio { get; set; } = 0.00M;

        public string Calificacion { get; set; }
        // Campos de control
        public DateTime FechaActualizacion { get; set; } 
        public string UsuarioActualizacion { get; set; } 

        // Relación con la entidad Calificacion
        public ICollection<Calificacion> Calificaciones { get; set; }
    }
}
