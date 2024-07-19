using NodaTime;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Dtos;

public class RelatorioMediaTarefasConcluidas
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public int TarefasConcluidas { get; set; }
}