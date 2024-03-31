using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PPADS_ERP_ESCOLAR.Models
{
    [Table("matricula")]
    public class Matricula
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idMatricula { get; set; }
        public int idAluno { get; set; }
        public int idSerie { get; set; }
        public int idTurma { get; set; }
        public int chamada { get; set; }

        public Matricula(int idAluno, int idSerie, int idTurma, int chamada)
        {
            this.idAluno = idAluno;
            this.idSerie = idSerie;
            this.idTurma = idTurma;
            this.chamada = chamada;
        }
    }
}
