using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;

namespace PPADS_ERP_ESCOLAR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        [HttpPost("send")]
        public IActionResult EnviarEmail(string email, string assunto, string mensagem)
        {
            try
            {
                var smtpClient = new SmtpClient("smtp.office365.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("ppads2024@outlook.com", "PP@DS2024"),
                    EnableSsl = true,
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress("ppads2024@outlook.com"),
                    Subject = assunto,
                    Body = mensagem,
                    IsBodyHtml = true,
                };

                mailMessage.To.Add(email);

                smtpClient.Send(mailMessage);

                return Ok(new { message = "Email enviado com sucesso!" });
            }
            catch (SmtpException ex)
            {
                return StatusCode(500, new { message = "Erro ao enviar o email.", details = ex.Message });
            }
        }
    }
}