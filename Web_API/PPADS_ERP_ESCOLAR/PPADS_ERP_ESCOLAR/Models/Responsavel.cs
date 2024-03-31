using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PPADS_ERP_ESCOLAR.Models
{
    [Table("responsavel")]
    public class Responsavel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idResponsavel { get; set; }
        public string nome { get; set; }
        public string email { get; set; }

        public Responsavel(string nome, string email) 
        {
            this.nome = nome;   
            this.email = email;
        }
    }
}
