using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PPADS_ERP_ESCOLAR.Models
{
    [Table("turma")]
    public class Turma
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idTurma { get; set; }
        public int idSerie { get; set; }
        public int qtdAlunos { get; set; }
        public string nome { get; set; }
        public string classe { get; set; }

        public Turma (int idSerie, int qtdAlunos, string nome, string classe) 
        {
            this.idSerie = idSerie;
            this.qtdAlunos = qtdAlunos;
            this.nome = nome;
            this.classe = classe;
        }
    }
    
}
