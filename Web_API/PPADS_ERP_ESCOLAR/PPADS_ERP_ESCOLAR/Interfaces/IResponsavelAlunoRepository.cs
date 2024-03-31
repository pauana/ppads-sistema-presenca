using PPADS_ERP_ESCOLAR.Models;

namespace PPADS_ERP_ESCOLAR.Interfaces
{
    public interface IResponsavelAlunoRepository
    {
        void Add(ResponsavelAluno responsavelAluno);

        void Update(ResponsavelAluno responsavelAluno);

        List<ResponsavelAluno> Get();

        ResponsavelAluno GetById(int id);

        void Delete(ResponsavelAluno responsavelAluno);
    }
}
