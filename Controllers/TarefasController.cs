using Microsoft.AspNetCore.Mvc;
using ListaDeTarefas.Data;
using ListaDeTarefas.Models;

namespace ListaDeTarefas.Controllers
{
    [ApiController]
    [Route("api/tarefas")]
    public class TarefasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TarefasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
                return Unauthorized("Você precisa fazer login");

            var tarefas = _context.Tarefas
                .Where(x => x.UserId == userId)
                .ToList();

            return Ok(tarefas);
        }

        [HttpPost]
        public IActionResult Create(Tarefa tarefa)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
                return Unauthorized("Você precisa fazer login");

            tarefa.UserId = userId.Value;

            _context.Tarefas.Add(tarefa);
            _context.SaveChanges();

            return Ok(tarefa);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Tarefa tarefa)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
                return Unauthorized("Você precisa fazer login");

            var tarefaBanco = _context.Tarefas
                .FirstOrDefault(x => x.Id == id && x.UserId == userId);

            if (tarefaBanco == null)
                return NotFound();

            tarefaBanco.Descricao = tarefa.Descricao;
            tarefaBanco.Status = tarefa.Status;

            _context.SaveChanges();

            return Ok(tarefaBanco);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
                return Unauthorized("Você precisa fazer login");

            var tarefa = _context.Tarefas
                .FirstOrDefault(x => x.Id == id && x.UserId == userId);

            if (tarefa == null)
                return NotFound();

            _context.Tarefas.Remove(tarefa);
            _context.SaveChanges();

            return Ok();
        }
    }
}