using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PPADS_ERP_ESCOLAR.Models
{
    [Table("responsavel_aluno")]
    public class ResponsavelAluno
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idResponsavelAluno { get; set; }
        public int idResponsavel { get; set; }
        public int idAluno { get; set; }

        public ResponsavelAluno(int idResponsavel, int idAluno) 
        {
            this.idResponsavel = idResponsavel;   
            this.idAluno = idAluno;
        }
    }
}
