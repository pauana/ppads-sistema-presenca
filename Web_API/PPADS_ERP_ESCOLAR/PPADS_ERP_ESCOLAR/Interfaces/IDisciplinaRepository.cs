using PPADS_ERP_ESCOLAR.Models;

namespace PPADS_ERP_ESCOLAR.Interfaces
{
    public interface IDisciplinaRepository
    {
        void Add(Disciplina disciplina);

        void Update(Disciplina disciplina);

        List<Disciplina> Get();

        Disciplina GetById(int id);

        void Delete(Disciplina disciplina);
    }
}
