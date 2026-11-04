namespace TuProyecto.Models
{
    public class PerfilViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Rol { get; set; }
        public string FotoRuta { get; set; }
        public string RestriccionesAlimenticias { get; set; }
        public IFormFile FotoArchivo { get; set; }
    }
}