using System.Data.Entity;
using PPADS_ERP_ESCOLAR.Interfaces;
using PPADS_ERP_ESCOLAR.Models;

namespace PPADS_ERP_ESCOLAR.Infra
{
    public class AuthServiceRepository : IAuthServiceRepository
    {
        private readonly DBConnection _context;

        public AuthServiceRepository(DBConnection context)
        {
            _context = context;
        }

        public async Task<bool> ValidateUser(string username, string password)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
            {
                return false;
            }
            return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
        }
    }
}
