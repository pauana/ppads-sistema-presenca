using PPADS_ERP_ESCOLAR.Models;

namespace PPADS_ERP_ESCOLAR.Interfaces
{
    public interface ITurmaProfessorRepository
    {
        void Add(TurmaProfessor turmaProfessor);

        void Update(TurmaProfessor turmaProfessor);

        List<TurmaProfessor> Get();

        TurmaProfessor GetById(int id);

        void Delete(TurmaProfessor turmaProfessor);
    }
}
