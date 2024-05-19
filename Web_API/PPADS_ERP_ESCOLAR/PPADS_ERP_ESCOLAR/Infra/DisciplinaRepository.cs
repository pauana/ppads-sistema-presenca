using PPADS_ERP_ESCOLAR.Interfaces;
using PPADS_ERP_ESCOLAR.Models;

namespace PPADS_ERP_ESCOLAR.Infra
{
    public class DisciplinaRepository : IDisciplinaRepository
    {
        private readonly DBConnection _context;

        public DisciplinaRepository(DBConnection context)
        {
            _context = context;
        }

        public void Add(Disciplina disciplina)
        {
            _context.Add(disciplina);
            _context.SaveChanges();
        }

        public void Delete(Disciplina disciplina)
        {
            _context.Remove(disciplina);
            _context.SaveChanges();
        }

        public List<Disciplina> Get()
        {
            return _context.Disciplinas.ToList();
        }

        public Disciplina GetById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    throw new Exception($"Id Inválido.");
                }
                var existingDisciplina = _context.Disciplinas.Find(id);
                return existingDisciplina;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter os dados da disciplina pelo ID: {ex.Message}", ex);
            }
        }

        public void Update(Disciplina disciplina)
        {
           _context.Update(disciplina);
            _context.SaveChanges();
        }
    }
}
