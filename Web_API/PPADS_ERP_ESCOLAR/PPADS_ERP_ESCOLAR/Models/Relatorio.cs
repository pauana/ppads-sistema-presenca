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

    public class TurmaItem
    {
        public string Turma { get; set; }
        public string Aluno { get; set; }
        public int Aulas { get; set; }
        public int Faltas { get; set; }
        public double Freq { get; set; }
    }

    public class ProfessorItem
    {
        public string Professor { get; set; }
        public string Turma { get; set; }
        public string Aluno { get; set; }
        public int Aulas { get; set; }
        public int Faltas { get; set; }
        public double Freq { get; set; }
    }

    public class DisciplinaItem
    {
        public string Disciplina { get; set; }
        public string Turma { get; set; }
        public string Aluno { get; set; }
        public int Aulas { get; set; }
        public int Faltas { get; set; }
        public double Freq { get; set; }
    }

    public class AlunoItem
    {
        public string Aluno { get; set; }
        public string Disciplina { get; set; }
        public string Professor { get; set; }
        public int Aulas { get; set; }
        public int Faltas { get; set; }
        public double Freq { get; set; }
    }
}