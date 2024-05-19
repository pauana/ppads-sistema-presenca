using PPADS_ERP_ESCOLAR.Interfaces;
using PPADS_ERP_ESCOLAR.Models;

namespace PPADS_ERP_ESCOLAR.Infra
{
    public class AulaRepository : IAulaRepository
    {
        private readonly DBConnection _context;

         public AulaRepository(DBConnection context)
        {
            _context = context;
        }

        public void Add(Aula aula)
        {
            _context.Add(aula);
            _context.SaveChanges();
        }

        public List<Aula> Get()
        {
            return _context.Aulas.ToList();
        }

        public Aula GetById(int id)
        {
            try {
                if(id == 0)
                {
                    throw new Exception($"Id Inválido");
                }
                var existingAula = _context.Aulas.Find(id);
                return existingAula;
            }
            catch(Exception ex)
            {
                throw new Exception($"Erro ao obter os dados da aula pelo ID: {ex.Message}", ex);
            }
        }

        public void Update(Aula aula)
        {
            _context.Aulas.Update(aula);
            _context.SaveChanges();
        }

        public void Delete(Aula aula)
        {            
                _context.Aulas.Remove(aula);
                _context.SaveChanges();          
        }
    }
}
