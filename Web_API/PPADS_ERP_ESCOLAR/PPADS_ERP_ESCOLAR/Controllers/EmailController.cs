using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;
using NETCore.MailKit.Core;
using PPADS_ERP_ESCOLAR.Infra;
using PPADS_ERP_ESCOLAR.Interfaces;
using PPADS_ERP_ESCOLAR.Models;

namespace PPADS_ERP_ESCOLAR.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmailController : ControllerBase
{
    private readonly IEmailServiceRepository _emailService;

    public EmailController(IEmailServiceRepository emailService)
    {
        _emailService = emailService;
    }
    [HttpPost]
    public IActionResult SendEmail(EmailDTO request)
    {
        _emailService.EnvioEmail(request);
        return Ok();
    }
}

