using PPADS_ERP_ESCOLAR.Models;

namespace PPADS_ERP_ESCOLAR.Interfaces
{
    public interface IEmailServiceRepository
    {
        public void EnvioEmail(EmailDTO request);
    }
}
