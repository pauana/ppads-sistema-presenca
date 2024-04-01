using PPADS_ERP_ESCOLAR.Interfaces;
using PPADS_ERP_ESCOLAR.Models;

namespace PPADS_ERP_ESCOLAR.Infra
{
    public class TurmaRepository : ITurmaRepository
    {
        private readonly DBConnection _context = new DBConnection();
        public void Add(Turma turma)
        {
            _context.Add(turma);
            _context.SaveChanges();
        }

        public void Delete(Turma turma)
        {
            _context.Remove(turma);
            _context.SaveChanges();
        }

        public List<Turma> Get()
        {
            return _context.Turmas.ToList();
        }

        public Turma GetById(int id)
        {
            try
            {
                if (id == 0)
                {
                    throw new Exception($"Id Inválido");
                }

                var turma = _context.Turmas.Find(id);

                if (turma == null)
                {
                    throw new Exception($"Turma não encontrada");
                }

                return turma;

            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter os dados da turma pelo ID: {ex.Message}", ex);
            }
        }

        public List<Turma> Get(int idSerie) 
        {
            return _context.Turmas
                      .Where(t => t.idSerie == idSerie)
                      .ToList();
        }

        public void Update(Turma turma)
        {
            _context.Update(turma);
            _context.SaveChanges();
        }
    }
}
