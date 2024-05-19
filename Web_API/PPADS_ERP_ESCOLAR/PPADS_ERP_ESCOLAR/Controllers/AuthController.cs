using Microsoft.AspNetCore.Mvc;
using PPADS_ERP_ESCOLAR.Infra;
using PPADS_ERP_ESCOLAR.Interfaces;
using PPADS_ERP_ESCOLAR.Models;
using PPADS_ERP_ESCOLAR.ViewModels;

namespace PPADS_ERP_ESCOLAR.Controllers;

[ApiController]
[Route("api/v1")]
public class AuthController : ControllerBase
{
    private readonly IAuthServiceRepository _authService;

    public AuthController(IAuthServiceRepository authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (await _authService.ValidateUser(request.Username, request.Password))
        {
            return Ok(new { message = "Login successful" });
        }
        return Unauthorized(new { message = "Acesso negado! Verifique o usuário e a senha." });
    }
}

public class LoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}
