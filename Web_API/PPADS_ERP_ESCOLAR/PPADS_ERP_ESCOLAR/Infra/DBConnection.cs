using Microsoft.EntityFrameworkCore;
using PPADS_ERP_ESCOLAR.Models;

namespace PPADS_ERP_ESCOLAR.Infra
{
    public class DBConnection : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=;database=;user=;password=", ServerVersion.AutoDetect("MySQL")); ;
        }

        public DbSet <Aula> Aulas { get; set; }
        public DbSet <Aluno> Alunos { get; set; }
        public DbSet <Disciplina> Disciplinas { get; set; } 
        public DbSet <Matricula> Matriculas { get; set; }
        public DbSet <Professor> Professores { get; set; }
        public DbSet <ProfessorDisciplina> ProfessoresDisciplina { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aula>()
                .Property(e => e.idAula)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Aluno>()
                .Property(e => e.idAluno)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Disciplina>()
                .Property(e => e.idDisciplina)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Matricula>()
                .Property(e => e.idMatricula)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Professor>()
                .Property(e => e.idProfessor)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<ProfessorDisciplina>()
                .Property(e => e.idProfDisciplina)
                .ValueGeneratedOnAdd();
        }
    }
}
