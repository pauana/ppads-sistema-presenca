using Microsoft.EntityFrameworkCore;
using PPADS_ERP_ESCOLAR.Interfaces;
using PPADS_ERP_ESCOLAR.Models;
using System.Data.Common;

namespace PPADS_ERP_ESCOLAR.Infra
{
    public class ProfessorDisciplinaRepository : IProfessorDisciplinaRepository
    {
        private readonly DBConnection _context = new DBConnection();
        public void Add(ProfessorDisciplina profDisc)
        {
            _context.Add(profDisc);
            _context.SaveChanges();
        }

        public void Delete(ProfessorDisciplina profDisc)
        {
            _context.Remove(profDisc);
            _context.SaveChanges();
        }

        public ProfessorDisciplina GetByProfId(int profId)
        {
            var professor = _context.ProfessoresDisciplina.Find(profId);
            return professor;
        }

        public List<ProfessorDisciplina> Get()
        {
            return _context.ProfessoresDisciplina.ToList();
        }

        public void Update(ProfessorDisciplina profDisc)
        {
           _context.Update(profDisc);
           _context.SaveChanges();
        }
    }
}
