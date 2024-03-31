using PPADS_ERP_ESCOLAR.Models;

namespace PPADS_ERP_ESCOLAR.Interfaces
{
    public interface ISerieRepository
    {
        void Add(Serie serie);

        void Update(Serie serie);

        List<Serie> Get();

        Serie GetById(int id);

        void Delete(Serie serie);
    }
}
