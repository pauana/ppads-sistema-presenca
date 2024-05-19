namespace PPADS_ERP_ESCOLAR.Interfaces
{
    public interface IAuthServiceRepository
    {
        Task<bool> ValidateUser(string username, string password);
    }
}
