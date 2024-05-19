using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Usuario
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public int IdProfessor { get; set; }

    public Usuario(string Username, string PasswordHash, int IdProfessor) 
        {
            this.Username = Username;   
            this.PasswordHash = PasswordHash;
            this.IdProfessor = IdProfessor;
        }
}
