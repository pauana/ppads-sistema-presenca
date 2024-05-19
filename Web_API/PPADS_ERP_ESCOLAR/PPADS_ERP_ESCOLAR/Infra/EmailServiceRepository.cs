
using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using PPADS_ERP_ESCOLAR.Interfaces;
using PPADS_ERP_ESCOLAR.Models;
using MailKit.Net.Smtp;
using MySqlX.XDevAPI;

namespace PPADS_ERP_ESCOLAR.Infra
{
    public class EmailServiceRepository : IEmailServiceRepository
    {
        private readonly IConfiguration _config;

        public EmailServiceRepository(IConfiguration config)
        {
            _config = config;
        }

        public void EnvioEmail(EmailDTO request)
        {
            var emailSettings = _config.GetSection("EmailSettings");
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(emailSettings["Email"]));
            email.To.Add(MailboxAddress.Parse(request.To));
            email.Subject = request.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

            try
            {
                using var smtp = new SmtpClient();

                smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;
                smtp.Connect(emailSettings["SmtpServer"], int.Parse(emailSettings["SmtpPort"]), SecureSocketOptions.StartTls);
                smtp.Authenticate(emailSettings["Email"], emailSettings["Password"]);
                smtp.Send(email);
                smtp.Disconnect(true);

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao enviar email.", ex);
            }

        }

    }
}
