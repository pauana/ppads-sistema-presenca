using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NETCore.MailKit.Core;
using PPADS_ERP_ESCOLAR.Infra;
using PPADS_ERP_ESCOLAR.Interfaces;
using System.Runtime.CompilerServices;

namespace PPADS_ERP_ESCOLAR.Controllers;

[ApiController]
[Route("api/v1/email")]
public class EmailController : ControllerBase
{

    private readonly IEmailServiceRepository _emailService;

    public EmailController(IEmailServiceRepository emailService)
    {
        _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
    }

    [HttpPost]
    [Route("email")]
    public async Task<IActionResult> EnviarEmail(string email, string assunto, string mensagem)
    {
        await _emailService.EnvioEmailAsync(email, assunto, mensagem);
        return Ok();
    }
}
