namespace Domain.EntitiesLayer
{
    public class Calificacion
    {
        public Guid Id { get; set; } 
        public string NicknameJugador { get; set; } 
        public decimal Puntuacion { get; set; } 

        // Relación con el videojuego
        public int VideojuegoId { get; set; } 
        public VideoJuego Videojuego { get; set; }

        // Campos de control
        public DateTime FechaActualizacion { get; set; } 
        public string UsuarioActualizacion { get; set; } 
    }
}
