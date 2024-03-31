using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PPADS_ERP_ESCOLAR.Models
{
    [Table("aula")]
    public class Aula
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idAula { get; set; }
        public int idTurma { get; set; }
        public int idProfessor { get; set; }
        public DateTime data { get; set; }
        public string periodo { get; set; } 
        public string conteudo { get; set; }

        public Aula(int idTurma, int idProfessor, DateTime data, string periodo, string conteudo)
        {            
            this.idTurma = idTurma;
            this.idProfessor = idProfessor;
            this.data = data;
            this.periodo = periodo;
            this.conteudo = conteudo;
        }
    }
}
