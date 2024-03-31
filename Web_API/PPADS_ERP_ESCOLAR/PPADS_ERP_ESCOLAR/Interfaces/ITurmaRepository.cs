using PPADS_ERP_ESCOLAR.Models;

namespace PPADS_ERP_ESCOLAR.Interfaces
{
    public interface ITurmaRepository
    {
        void Add(Turma turma);

        void Update(Turma turma);

        List<Turma> Get();

        Turma GetById(int id);

        void Delete(Turma turma);
    }
}
