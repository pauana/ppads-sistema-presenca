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

        public void Update(Matricula matricula)
        {
            _context.Update(matricula);
            _context.SaveChanges();
        }
    }
}
