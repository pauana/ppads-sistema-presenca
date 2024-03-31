using PPADS_ERP_ESCOLAR.Models;

namespace PPADS_ERP_ESCOLAR.Interfaces
{
    public interface IRegistroPresencaRepository
    {
        void Add(RegistroPresenca registroPresenca);

        void Update(RegistroPresenca registroPresenca);

        List<RegistroPresenca> Get();

        RegistroPresenca GetById(int id);

        void Delete(RegistroPresenca registroPresenca);
    }
}
