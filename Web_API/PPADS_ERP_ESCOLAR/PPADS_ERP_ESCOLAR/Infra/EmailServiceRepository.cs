
using PPADS_ERP_ESCOLAR.Interfaces;
using System.Net;
using System.Net.Mail;

namespace PPADS_ERP_ESCOLAR.Infra
{
    public class EmailServiceRepository : IEmailServiceRepository
    {
        public Task EnvioEmailAsync(string email, string assunto, string mensagem)
        {
            //endereço de email do sender
            var mail = "ppads2024@outlook.com";
            //senha do mesmo email
            var password = "PP@DS2024";

            var client = new SmtpClient("smtp-mail.outlook.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(mail, password)
            };

            return client.SendMailAsync(new MailMessage(from: mail, to: email, assunto, mensagem));

        }
    }
}
