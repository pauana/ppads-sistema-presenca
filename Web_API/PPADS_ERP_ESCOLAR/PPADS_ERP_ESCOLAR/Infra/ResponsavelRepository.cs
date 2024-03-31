using PPADS_ERP_ESCOLAR.Interfaces;
using PPADS_ERP_ESCOLAR.Models;

namespace PPADS_ERP_ESCOLAR.Infra
{
    public class ResponsavelRepository : IResponsavelRepository
    {
        private readonly DBConnection _context = new DBConnection();

        public void Add(Responsavel responsavel)
        {
            _context.Add(responsavel);
            _context.SaveChanges();
        }

        public List<Responsavel> Get()
        {
            return _context.Responsaveis.ToList();
        }

        public Responsavel GetById(int idResponsavel)
        {
            try
            {
                if (idResponsavel == 0)
                {
                    throw new Exception($"Id Inválido");
                }

                var responsavel = _context.Responsaveis.Find(idResponsavel);

                if (responsavel == null)
                {
                    throw new Exception($"Responsavel não encontrado");
                }

                return responsavel;

            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter os dados do Responsável pelo ID: {ex.Message}", ex);
            }
        }

        public void Delete(Responsavel responsavel)
        {
            _context.Remove(responsavel);
            _context.SaveChanges();
        }

        public void Update(Responsavel responsavel)
        {
            _context.Responsaveis.Update(responsavel);
            _context.SaveChanges();
        }
    }
}
