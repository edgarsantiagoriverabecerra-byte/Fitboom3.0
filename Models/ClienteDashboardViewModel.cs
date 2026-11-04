namespace SaludoWebApp.Models
{
    public class ClienteDashboardViewModel
    {
        public string Nombre { get; set; }
        public string FotoUrl { get; set; }
        public int ProgresoPorcentaje { get; set; }

        // Macros
        public int CaloriasActuales { get; set; }
        public int CaloriasTotales { get; set; }
        public int ProteinaActual { get; set; }
        public int ProteinaMeta { get; set; }
        public int CarboActual { get; set; }
        public int CarboMeta { get; set; }
        public int GrasaActual { get; set; }
        public int GrasaMeta { get; set; }

        // Hidratación
        public int VasosAgua { get; set; }
        public int VasosMeta { get; set; } = 8;

        // Listado de alimentos consumidos hoy
        public List<ConsumoAlimentoDto> AlimentosHoy { get; set; } = new();
    }

    public class ConsumoAlimentoDto
    {
        public string Nombre { get; set; }
        public string TipoComida { get; set; } // Desayuno, Almuerzo, Cena, Snack
        public int CantidadGramos { get; set; }
        public int Calorias { get; set; }
        public string Icono { get; set; }
    }
}