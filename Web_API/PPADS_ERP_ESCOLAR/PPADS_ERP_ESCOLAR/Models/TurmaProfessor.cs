using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PPADS_ERP_ESCOLAR.Models
{
    [Table("turma_professor")]
    public class TurmaProfessor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idTurmaProfessor { get; set; }
        public int idTurma { get; set; }
        public int idProfessor { get; set; }

        public TurmaProfessor (int idTurma, int idProfessor) 
        {
            this.idTurma = idTurma;   
            this.idProfessor = idProfessor;
        }
    }
}
