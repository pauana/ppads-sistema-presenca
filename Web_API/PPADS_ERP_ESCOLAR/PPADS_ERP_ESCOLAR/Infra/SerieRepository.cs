using PPADS_ERP_ESCOLAR.Interfaces;
using PPADS_ERP_ESCOLAR.Models;

namespace PPADS_ERP_ESCOLAR.Infra
{
    public class SerieRepository : ISerieRepository
    {
        private readonly DBConnection _context;

        public SerieRepository(DBConnection context)
        {
            _context = context;
        }

        public void Add(Serie serie)
        {
            _context.Add(serie);
            _context.SaveChanges();
        }

        public List<Serie> Get()
        {
            return _context.Series.ToList();
        }

        public Serie GetById(int idSerie)
        {
            try
            {
                if (idSerie == 0)
                {
                    throw new Exception($"Id Inválido");
                }

                var serie = _context.Series.Find(idSerie);

                if (serie == null)
                {
                    throw new Exception($"Série não encontrada");
                }

                return serie;

            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter os dados da Série pelo ID: {ex.Message}", ex);
            }
        }

        public void Delete(Serie serie)
        {
            _context.Remove(serie);
            _context.SaveChanges();
        }

        public void Update(Serie serie)
        {
            _context.Series.Update(serie);
            _context.SaveChanges();
        }
    }
}
