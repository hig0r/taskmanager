using NodaTime;
using TaskManager.Domain.Entities;

namespace TaskManager.Api.Dtos;

public class CriarTarefaDto
{
    public int ProjetoId { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public LocalDate Vencimento { get; set; }
    public TarefaPrioridade Prioridade { get; set; }
}