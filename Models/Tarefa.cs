namespace ListaDeTarefas.Models
{
    public class Tarefa
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public bool Status { get; set; }
        public int UserId { get; set; }
    }
}
