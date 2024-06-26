﻿using Microsoft.EntityFrameworkCore;
using PPADS_ERP_ESCOLAR.Models;

namespace PPADS_ERP_ESCOLAR.Infra
{
    public class DBConnection : DbContext
    {
        public DBConnection(DbContextOptions<DBConnection> options) : base(options)
        {
        }
        public DbSet <Aula> Aulas { get; set; }
        public DbSet <Aluno> Alunos { get; set; }
        public DbSet <Disciplina> Disciplinas { get; set; } 
        public DbSet <Matricula> Matriculas { get; set; }
        public DbSet <Professor> Professores { get; set; }
        public DbSet <ProfessorDisciplina> ProfessoresDisciplina { get; set; }
        public DbSet <Responsavel> Responsaveis { get; set; }
        public DbSet <ResponsavelAluno> ResponsaveisAluno { get; set; }
        public DbSet <Serie> Series { get; set; }
        public DbSet <Turma> Turmas { get; set; }
        public DbSet <TurmaProfessor> TurmasProfessor { get; set; }
        public DbSet <RegistroPresenca> RegistrosPresenca { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();

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
            
            modelBuilder.Entity<Matricula>()
                        .HasIndex(m => m.idSerie);

            modelBuilder.Entity<Matricula>()
                        .HasIndex(m => m.idTurma);

            modelBuilder.Entity<Professor>()
                .Property(e => e.idProfessor)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<ProfessorDisciplina>()
                .Property(e => e.idProfDisciplina)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Responsavel>()
                .Property(e => e.idResponsavel)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<ResponsavelAluno>()
                .Property(e => e.idResponsavelAluno)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Serie>()
                .Property(e => e.idSerie)
                .ValueGeneratedOnAdd(); 

            modelBuilder.Entity<Turma>()
                .Property(e => e.idTurma)
                .ValueGeneratedOnAdd(); 
            
            modelBuilder.Entity<Turma>()
                        .HasIndex(t => t.idSerie);

            modelBuilder.Entity<TurmaProfessor>()
                .Property(e => e.idTurmaProfessor)
                .ValueGeneratedOnAdd();  

            modelBuilder.Entity<RegistroPresenca>()
                .Property(e => e.idRegistroPresenca)
                .ValueGeneratedOnAdd();  

            modelBuilder.Entity<RegistroPresenca>()
                        .HasIndex(r => r.idMatricula);
                        
        }
    }
}
