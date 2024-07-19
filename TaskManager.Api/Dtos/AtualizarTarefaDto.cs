using NodaTime;
using TaskManager.Domain.Entities;

namespace TaskManager.Api.Dtos;

public class AtualizarTarefaDto
{
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public LocalDate Vencimento { get; set; }
    public TarefaStatus Status { get; set; }
}