
using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using PPADS_ERP_ESCOLAR.Interfaces;
using PPADS_ERP_ESCOLAR.Models;
using System.Net;
using MailKit.Net.Smtp;

namespace PPADS_ERP_ESCOLAR.Infra
{
    public class EmailServiceRepository : IEmailServiceRepository
    {
        private readonly IConfiguration _config;

        //public EmailServiceRepository(IConfiguration config)
        //{
        //    _config = config;
        //}

        public void EnvioEmail(EmailDTO request)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("ppads2024@outlook.com"));
            email.To.Add(MailboxAddress.Parse(request.To));
            email.Subject = request.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.office365.com", 587, true);
            smtp.Authenticate("´ppads2024@outlook.com", "PP@DS2024");
            smtp.Send(email);
            smtp.Disconnect(true);
            
        }

    }
}
