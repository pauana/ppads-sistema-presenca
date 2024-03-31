using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PPADS_ERP_ESCOLAR.Models
{
    [Table("professor")]
    public class Professor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idProfessor { get; set; }
        public string nome { get; set; }
        public string ativo { get; set; }
        
        public Professor(string nome, string ativo)
        {
            this.nome = nome;
            this.ativo = ativo;
        }
    }
}
