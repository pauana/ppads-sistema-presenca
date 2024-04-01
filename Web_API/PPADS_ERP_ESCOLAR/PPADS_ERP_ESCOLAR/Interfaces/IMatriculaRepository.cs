using PPADS_ERP_ESCOLAR.Models;

namespace PPADS_ERP_ESCOLAR.Interfaces
{
    public interface IMatriculaRepository
    {
        void Add(Matricula matricula);

        void Update(Matricula matricula);

        List<Matricula> Get();

        Matricula GetById(int id);

        List<Matricula> Get(char tipo, int id);

        List<int> Get( int idTurma);

        void Delete(Matricula matricula);
    }
}
