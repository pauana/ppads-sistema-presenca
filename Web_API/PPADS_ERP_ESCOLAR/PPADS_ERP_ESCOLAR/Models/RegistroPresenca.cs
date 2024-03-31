using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PPADS_ERP_ESCOLAR.Models
{
    [Table("registro_presenca")]
    public class RegistroPresenca
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idRegistroPresenca { get; set; }
        public int idMatricula { get; set; }
        public int idAula { get; set; }
        public string presenca { get; set; }

        public RegistroPresenca(int idMatricula, int idAula, string presenca) 
        {
            this.idMatricula = idMatricula;
            this.idAula = idAula;
            this.presenca = presenca;
        }
    }
}
