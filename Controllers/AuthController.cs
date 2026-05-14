using ListaDeTarefas.Data;
using Microsoft.AspNetCore.Mvc;

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
            {
                return Unauthorized("Email ou senha invalidos");
            }

            HttpContext.Session.SetString("IdLogado", user.Id.ToString());

            Response.Cookies.Append("IdLogado", user.Id.ToString());

            return Ok("Login realizado");
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            Response.Cookies.Delete("IdLogado");

            return Ok("Logout realizado");
        }
    }
}