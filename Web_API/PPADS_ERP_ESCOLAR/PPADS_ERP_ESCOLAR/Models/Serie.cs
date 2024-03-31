using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PPADS_ERP_ESCOLAR.Models
{
    [Table("serie")]
    public class Serie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idSerie { get; set; }
        public int ano { get; set; }
        public string nomeSerie { get; set; }
        public int qtdeTurmas { get; set; }
        public int vagas { get; set; }

        public Serie(int ano, string nomeSerie, int qtdeTurmas, int vagas) 
        {
           this.ano = ano;
           this.nomeSerie = nomeSerie;
           this.qtdeTurmas = qtdeTurmas;
           this.vagas = vagas;
        }
    }
}
