using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PPADS_ERP_ESCOLAR.Models
{
    [Table("disciplina")]
    public class Disciplina
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idDisciplina { get; set; }

        public string materia { get; set; }

        public Disciplina(string materia)
        {
            this.materia = materia;
        }
    }
    
}
