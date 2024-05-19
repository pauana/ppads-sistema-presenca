using PPADS_ERP_ESCOLAR.Models;

namespace PPADS_ERP_ESCOLAR.Interfaces
{
    public interface IAuthServiceRepository
    {
        Task<Usuario> ValidateUser(string username, string password);
    }
}
