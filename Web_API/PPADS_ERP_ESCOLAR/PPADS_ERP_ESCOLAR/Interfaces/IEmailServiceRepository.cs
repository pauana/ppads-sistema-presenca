namespace PPADS_ERP_ESCOLAR.Interfaces
{
    public interface IEmailServiceRepository
    {
        Task EnvioEmailAsync(string email, string assunto, string mensagem);
    }
}
