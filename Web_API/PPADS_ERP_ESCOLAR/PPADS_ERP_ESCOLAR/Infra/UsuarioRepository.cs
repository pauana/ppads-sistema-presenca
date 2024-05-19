using PPADS_ERP_ESCOLAR.Interfaces;
using PPADS_ERP_ESCOLAR.Models;

namespace PPADS_ERP_ESCOLAR.Infra
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DBConnection _context;

        public void Add(Usuario usuario)
        {
            _context.Add(usuario);
            _context.SaveChanges();
        }


    }
}
