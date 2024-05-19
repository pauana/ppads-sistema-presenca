using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PPADS_ERP_ESCOLAR.Models
{
    [Table("usuario")]
    public class Usuario
    {   
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public int idProfessor { get; set; }

        public Usuario() { }

        public Usuario(string Username, string PasswordHash, int idProfessor) 
            {
                this.Username = Username;   
                this.PasswordHash = PasswordHash;
                this.idProfessor = idProfessor;
            }
    }
}
