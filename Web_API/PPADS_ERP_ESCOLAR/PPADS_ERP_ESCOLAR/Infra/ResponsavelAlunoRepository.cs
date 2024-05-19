using Microsoft.EntityFrameworkCore;
using PPADS_ERP_ESCOLAR.Interfaces;
using PPADS_ERP_ESCOLAR.Models;
using System.Data.Common;

namespace PPADS_ERP_ESCOLAR.Infra
{
    public class ResponsavelAlunoRepository : IResponsavelAlunoRepository
    {
        private readonly DBConnection _context;

        public ResponsavelAlunoRepository(DBConnection context)
        {
            _context = context;
        }
        public void Add(ResponsavelAluno responsavelAluno)
        {
            _context.Add(responsavelAluno);
            _context.SaveChanges();
        }

        public void Delete(ResponsavelAluno responsavelAluno)
        {
            _context.Remove(responsavelAluno);
            _context.SaveChanges();
        }

        public ResponsavelAluno GetById(int idResponsavelAluno)
        {
            var responsavelAluno = _context.ResponsaveisAluno.Find(idResponsavelAluno);
            return responsavelAluno;
        }

        public List<ResponsavelAluno> Get()
        {
            return _context.ResponsaveisAluno.ToList();
        }

        public void Update(ResponsavelAluno responsavelAluno)
        {
           _context.Update(responsavelAluno);
           _context.SaveChanges();
        }
    }
}
