using PPADS_ERP_ESCOLAR.Models;

namespace PPADS_ERP_ESCOLAR.Interfaces
{
    public interface IAlunoRepository
    {
        void Add(Aluno aluno);

        void Update(Aluno aluno);

        List<Aluno> Get();

        Aluno GetById(int id);

        void Delete(Aluno aluno);
    }
}
