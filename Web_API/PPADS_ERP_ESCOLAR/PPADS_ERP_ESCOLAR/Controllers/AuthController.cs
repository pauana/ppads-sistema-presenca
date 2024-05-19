using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PPADS_ERP_ESCOLAR.Infra;
using PPADS_ERP_ESCOLAR.Interfaces;
using PPADS_ERP_ESCOLAR.Models;
using PPADS_ERP_ESCOLAR.Services;

namespace PPADS_ERP_ESCOLAR.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServiceRepository _authService;
        private readonly DBConnection _context;
        private readonly TokenService _tokenService;

        public AuthController(IAuthServiceRepository authService, DBConnection context, TokenService tokenService)
        {
            _authService = authService;
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _authService.ValidateUser(request.Username, request.Password);
            if (user != null)
            {
                var token = _tokenService.GenerateToken(request.Username);
                return Ok(new { 
                                    token, 
                                    username = user.Username, 
                                    idProfessor = user.idProfessor 
                                });
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
        [HttpGet("verify-token")]
        [Authorize]
        public IActionResult VerifyToken()
        {
            return Ok(new { message = "Token is valid." });
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
