using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PPADS_ERP_ESCOLAR.Models
{
    public class Filtros
    {
        public DateTime? DataIni { get; set; }
        public DateTime? DataFim { get; set; }
        [Required]
        public int AnoLetivo { get; set; }
        public string Periodo { get; set; }
        public int? Turma { get; set; }
        [Required]
        public string Agrupar { get; set; } 
    }
}