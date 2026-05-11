using Microsoft.EntityFrameworkCore;
using ListaDeTarefas.Models;
using System.Collections.Generic;

namespace ListaDeTarefas.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Tarefa> Tarefas { get; set; }
    }
}
