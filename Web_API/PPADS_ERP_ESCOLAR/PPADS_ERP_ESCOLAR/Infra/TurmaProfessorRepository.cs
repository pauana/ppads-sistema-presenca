using Microsoft.EntityFrameworkCore;
using PPADS_ERP_ESCOLAR.Interfaces;
using PPADS_ERP_ESCOLAR.Models;
using System.Data.Common;

namespace PPADS_ERP_ESCOLAR.Infra
{
    public class TurmaProfessorRepository : ITurmaProfessorRepository
    {
        private readonly DBConnection _context = new DBConnection();
        public void Add(TurmaProfessor turmaProfessor)
        {
            _context.Add(turmaProfessor);
            _context.SaveChanges();
        }

        public void Delete(TurmaProfessor turmaProfessor)
        {
            _context.Remove(turmaProfessor);
            _context.SaveChanges();
        }

        public TurmaProfessor GetById(int idTurmaProfessor)
        {
            var turmasProfessor = _context.TurmasProfessor.Find(idTurmaProfessor);
            return turmasProfessor;
        }

        public List<TurmaProfessor> Get()
        {
            return _context.TurmasProfessor.ToList();
        }

        public void Update(TurmaProfessor turmaProfessor)
        {
           _context.Update(turmaProfessor);
           _context.SaveChanges();
        }
    }
}
