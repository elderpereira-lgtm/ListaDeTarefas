using ListaDeTarefas.Data;
using ListaDeTarefas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            return Ok(_context.Tarefas.ToList());
        }

        [HttpGet("usuario")]
        public IActionResult TarefasUsuario()
        {
            var sessaoUsuario = HttpContext.Session.GetString("IdLogado");

            if (sessaoUsuario == null)
            {
                return Unauthorized("Faca login antes");
            }

            var idLogado = Request.Cookies["IdLogado"];

            if (idLogado != null)
            {
                var resultado = from u in _context.Users
                                join t in _context.Tarefas
                                on u.Id equals t.UserId
                                where u.Id == int.Parse(idLogado)
                                select new
                                {
                                    Usuario = u.Nome,
                                    u.Email,
                                    Tarefa = t.Descricao,
                                    t.Status
                                };

                return Ok(resultado.ToList());
            }

            return Unauthorized("Faca login antes");
        }

        [HttpPost]
        public IActionResult Create(Tarefa tarefa)
        {
            var idLogado = Request.Cookies["IdLogado"];

            if (idLogado == null)
            {
                return Unauthorized("Faca login antes");
            }

            tarefa.UserId = int.Parse(idLogado);

            _context.Tarefas.Add(tarefa);
            _context.SaveChanges();

            return Ok(tarefa);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Tarefa tarefaAtualizada)
        {
            var tarefa = _context.Tarefas.Find(id);

            if (tarefa == null)
            {
                return NotFound("Tarefa nao encontrada");
            }

            tarefa.Descricao = tarefaAtualizada.Descricao;
            tarefa.Status = tarefaAtualizada.Status;

            _context.SaveChanges();

            return Ok(tarefa);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var tarefa = _context.Tarefas.Find(id);

            if (tarefa == null)
            {
                return NotFound("Tarefa nao encontrada");
            }

            _context.Tarefas.Remove(tarefa);
            _context.SaveChanges();

            return Ok("Tarefa removida");
        }
    }
}