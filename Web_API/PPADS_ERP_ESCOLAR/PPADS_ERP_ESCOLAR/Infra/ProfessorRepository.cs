using PPADS_ERP_ESCOLAR.Interfaces;
using PPADS_ERP_ESCOLAR.Models;

namespace PPADS_ERP_ESCOLAR.Infra
{
    public class ProfessorRepository : IProfessorRepository
    {
        private readonly DBConnection _context;

        public ProfessorRepository(DBConnection context)
        {
            _context = context;
        }
        public void Add(Professor professor)
        {
            _context.Add(professor);
            _context.SaveChanges();
        }

        public void Delete(Professor professor)
        {
            _context.Remove(professor);
            _context.SaveChanges();
        }

        public List<Professor> Get()
        {
            return _context.Professores.ToList();
        }

        public Professor GetById(int id)
        {
            try
            {
                if (id == 0)
                {
                    throw new Exception($"Id Inválido");
                }

                var professor = _context.Professores.Find(id);

                if (professor == null)
                {
                    throw new Exception($"Professor não encontrado");
                }

                return professor;

            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter os dados do professor pelo ID: {ex.Message}", ex);
            }
        }

        public void Update(Professor professor)
        {
            _context.Update(professor);
            _context.SaveChanges();
        }
    }
}
