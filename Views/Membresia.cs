using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FITBOOM.Models
{
    [Table("Membresias")] // Nombre exacto de la tabla en SQL Server
    public class Membresia
    {
        [Key]
        public int IdMembresia { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int DuracionDias { get; set; }
    }
}