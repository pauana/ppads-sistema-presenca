using PPADS_ERP_ESCOLAR.Interfaces;
using PPADS_ERP_ESCOLAR.Models;

namespace PPADS_ERP_ESCOLAR.Infra
{
    public class AlunoRepository : IAlunoRepository
    {
        private readonly DBConnection _context;

         public AlunoRepository(DBConnection context)
        {
             _context = context;
        }

        public void Add(Aluno aluno)
        {
            _context.Add(aluno);
            _context.SaveChanges();
        }

        public List<Aluno> Get()
        {
            return _context.Alunos.ToList();
        }

        public Aluno GetById(int idAluno)
        {
            try
            {
                if (idAluno == 0)
                {
                    throw new Exception($"Id Inválido");
                }

                var aluno = _context.Alunos.Find(idAluno);

                if (aluno == null)
                {
                    throw new Exception($"Aluno não encontrado");
                }

                return aluno;

            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter os dados do aluno pelo ID: {ex.Message}", ex);
            }
        }

        public void Delete(Aluno aluno)
        {
            _context.Remove(aluno);
            _context.SaveChanges();
        }

        public void Update(Aluno aluno)
        {
            _context.Alunos.Update(aluno);
            _context.SaveChanges();
        }
    }
}
