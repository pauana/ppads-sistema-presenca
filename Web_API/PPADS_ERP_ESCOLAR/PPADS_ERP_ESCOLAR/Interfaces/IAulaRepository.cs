using PPADS_ERP_ESCOLAR.Models;

namespace PPADS_ERP_ESCOLAR.Interfaces
{
    public interface IAulaRepository
    {
        void Add(Aula aula);

        List<Aula> Get();

        Aula GetById(int idAula);

        void Update(Aula aula);

        void Delete(Aula aula);
        
    }
}
