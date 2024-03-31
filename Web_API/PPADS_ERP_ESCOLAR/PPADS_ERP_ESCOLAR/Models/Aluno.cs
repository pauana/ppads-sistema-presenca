using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PPADS_ERP_ESCOLAR.Models
{
    [Table("aluno")]
    public class Aluno
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idAluno { get; set; }
        public string nome { get; set; }
        public string ra { get; set; }

        public Aluno(string nome, string ra) 
        {
            this.nome = nome;   
            this.ra = ra;
        }
    }
}
