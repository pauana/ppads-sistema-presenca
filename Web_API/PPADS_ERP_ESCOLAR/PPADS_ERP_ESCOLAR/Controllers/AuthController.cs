using Microsoft.AspNetCore.Mvc;
using PPADS_ERP_ESCOLAR.Infra;
using PPADS_ERP_ESCOLAR.Interfaces;
using PPADS_ERP_ESCOLAR.Models;
using PPADS_ERP_ESCOLAR.ViewModels;

namespace PPADS_ERP_ESCOLAR.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServiceRepository _authService;
        private readonly DBConnection _context;

        public AuthController(IAuthServiceRepository authService, DBConnection context)
        {
            _authService = authService;
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (await _authService.ValidateUser(request.Username, request.Password))
            {
                return Ok(new { message = "Logado com sucesso!" });
            }
            return Unauthorized(new { message = "Acesso negado! Verifique o usuário e a senha." });
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var user = new Usuario
            {
                Username = request.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                idProfessor = request.idProfessor
            };

            _context.Usuarios.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Usuário registrado com sucesso!" });
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class RegisterRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int idProfessor { get; set; }
    }
}
