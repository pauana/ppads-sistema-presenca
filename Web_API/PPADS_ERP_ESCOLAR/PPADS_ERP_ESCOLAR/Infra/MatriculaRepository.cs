using PPADS_ERP_ESCOLAR.Interfaces;
using PPADS_ERP_ESCOLAR.Models;

namespace PPADS_ERP_ESCOLAR.Infra
{
    public class MatriculaRepository : IMatriculaRepository
    {
        private readonly DBConnection _context = new DBConnection();
        public void Add(Matricula matricula)
        {
            _context.Add(matricula);
            _context.SaveChanges();
        }

        public void Delete(Matricula matricula)
        {
            _context.Matriculas.Remove(matricula);
            _context.SaveChanges();
        }

        public List<Matricula> Get()
        {
            return _context.Matriculas.ToList();
        }

        public Matricula GetById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    throw new Exception($"Id Inválido.");
                }
                var existingMatricula = _context.Matriculas.Find(id);
                return existingMatricula;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter os dados da matrícula pelo ID: {ex.Message}", ex);
            }
        }

        public List<Matricula> Get(char tipo, int id)
        {
            if (tipo == 'S')
            {
                return _context.Matriculas
                      .Where(m => m.idSerie == id)
                      .ToList();
            } else if (tipo == 'T')
            {
                return _context.Matriculas
                      .Where(m => m.idTurma == id)
                      .ToList();
            }
            return new List<Matricula>();
        }

        public List<int> Get(int idTurma)
        {
            return _context.Matriculas
                      .Where(m => m.idTurma == idTurma)
                      .Select(m => m.idMatricula)
                      .ToList();
        }

        public void Update(Matricula matricula)
        {
            _context.Update(matricula);
            _context.SaveChanges();
        }
    }
}
