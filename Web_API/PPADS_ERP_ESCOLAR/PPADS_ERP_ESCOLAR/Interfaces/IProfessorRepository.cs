using PPADS_ERP_ESCOLAR.Models;

namespace PPADS_ERP_ESCOLAR.Interfaces
{
    public interface IProfessorRepository
    {
        void Add(Professor professor);

        void Update(Professor professor);

        List<Professor> Get();

        Professor GetById(int id);

        void Delete(Professor professor);
    }
}
