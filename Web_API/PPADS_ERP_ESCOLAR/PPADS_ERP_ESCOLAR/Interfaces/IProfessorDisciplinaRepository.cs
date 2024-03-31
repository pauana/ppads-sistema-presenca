using PPADS_ERP_ESCOLAR.Models;

namespace PPADS_ERP_ESCOLAR.Interfaces
{
    public interface IProfessorDisciplinaRepository
    {
        void Add(ProfessorDisciplina profDisc);

        void Update(ProfessorDisciplina profdisc);

        List<ProfessorDisciplina> Get();

        ProfessorDisciplina GetByProfId(int profId);

        void Delete(ProfessorDisciplina profDisc);
    }
}
