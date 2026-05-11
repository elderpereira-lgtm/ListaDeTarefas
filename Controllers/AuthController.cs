using Microsoft.AspNetCore.Mvc;
using ListaDeTarefas.Data;

namespace ListaDeTarefas.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public IActionResult Login(string email, string senha)
        {
            var user = _context.Users
                .FirstOrDefault(x => x.Email == email && x.Senha == senha);

            if (user == null)
                return Unauthorized("Email ou senha inválidos");

            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("Nome", user.Nome);

            return Ok("Login realizado com sucesso");
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return Ok("Logout realizado");
        }
    }
}
