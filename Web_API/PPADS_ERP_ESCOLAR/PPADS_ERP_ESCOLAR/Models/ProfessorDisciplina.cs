using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PPADS_ERP_ESCOLAR.Models
{
    [Table("professor_disciplina")]
    public class ProfessorDisciplina
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idProfDisciplina { get; set; }
        public int idProfessor { get; set; }
        public int idDisciplina { get; set; }

        public ProfessorDisciplina(int idProfessor, int idDisciplina)
        {
            this.idProfessor = idProfessor;
            this.idDisciplina = idDisciplina;
        }
    }
}
