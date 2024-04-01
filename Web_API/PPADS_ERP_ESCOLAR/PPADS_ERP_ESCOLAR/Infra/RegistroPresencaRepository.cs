using PPADS_ERP_ESCOLAR.Interfaces;
using PPADS_ERP_ESCOLAR.Models;

namespace PPADS_ERP_ESCOLAR.Infra
{
    public class RegistroPresencaRepository : IRegistroPresencaRepository
    {
        private readonly DBConnection _context = new DBConnection();
        public void Add(RegistroPresenca registroPresenca)
        {
            _context.Add(registroPresenca);
            _context.SaveChanges();
        }

        public void Delete(RegistroPresenca registroPresenca)
        {
            _context.Remove(registroPresenca);
            _context.SaveChanges();
        }

        public List<RegistroPresenca> Get()
        {
            return _context.RegistrosPresenca.ToList();
        }

        public RegistroPresenca GetById(int id)
        {
            try
            {
                if (id == 0)
                {
                    throw new Exception($"Id Inválido");
                }

                var registroPresenca = _context.RegistrosPresenca.Find(id);

                if (registroPresenca == null)
                {
                    throw new Exception($"Registro não encontrado");
                }

                return registroPresenca;

            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter os dados do registro pelo ID: {ex.Message}", ex);
            }
        }

        public List<RegistroPresenca> Get(List<int> idMatriculas)
        {
            return _context.RegistrosPresenca
                      .Where(m => idMatriculas.Contains(m.idMatricula))
                      .ToList();
        }

        public void Update(RegistroPresenca registroPresenca)
        {
            _context.Update(registroPresenca);
            _context.SaveChanges();
        }
    }
}
