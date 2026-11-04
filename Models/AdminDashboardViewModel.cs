namespace Fitboom_2._0.Models
{
    public class Profesional
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Especialidad { get; set; } = string.Empty; // Ej: NUTRICIONISTA, MASTER COACH, FISIOTERAPEUTA
        public string? FotoUrl { get; set; }
        public bool Activo { get; set; } = true;
    }

    public class AdminDashboardViewModel
    {
        public List<Profesional> StaffProfesional { get; set; } = new List<Profesional>();
        // Aquí puedes agregar más listas cuando conectes las demás tablas (Membresías, Logs, etc.)
    }
}