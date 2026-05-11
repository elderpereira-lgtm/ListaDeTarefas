using Microsoft.AspNetCore.Mvc;
using ListaDeTarefas.Data;
using ListaDeTarefas.Models;

namespace ListaDeTarefas.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Users.ToList());
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok(user);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, User user)
        {
            var usuario = _context.Users.Find(id);

            if (usuario == null)
                return NotFound();

            usuario.Nome = user.Nome;
            usuario.Email = user.Email;
            usuario.Senha = user.Senha;

            _context.SaveChanges();

            return Ok(usuario);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var usuario = _context.Users.Find(id);

            if (usuario == null)
                return NotFound();

            _context.Users.Remove(usuario);
            _context.SaveChanges();

            return Ok();
        }
    }
}
