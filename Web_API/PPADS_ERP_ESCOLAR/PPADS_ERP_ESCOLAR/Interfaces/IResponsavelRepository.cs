using PPADS_ERP_ESCOLAR.Models;

namespace PPADS_ERP_ESCOLAR.Interfaces
{
    public interface IResponsavelRepository
    {
        void Add(Responsavel responsavel);

        void Update(Responsavel responsavel);

        List<Responsavel> Get();

        Responsavel GetById(int id);

        void Delete(Responsavel responsavel);
    }
}
