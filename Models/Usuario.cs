using System.ComponentModel.DataAnnotations;

namespace FITBOOM.Models
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Contraseña { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
        public string? FotoRuta { get; set; }
        public string? RestriccionesAlimenticias { get; set; }
        public bool Activo { get; set; } = true;
    }

    public class AdminDashboardViewModel
    {
        public List<Usuario> StaffProfesional { get; set; } = new List<Usuario>();
    }
}