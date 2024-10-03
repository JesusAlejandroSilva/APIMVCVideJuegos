namespace MVCGAMES.Models
{
    public class VideoJuegoViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Compania { get; set; }
        public int AnioLanzamiento { get; set; }
        public decimal Precio { get; set; }
        public decimal Puntaje { get; set; }
    }
}
