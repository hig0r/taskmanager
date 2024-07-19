using System.Collections.Immutable;
using NodaTime;
using TaskManager.Domain.Common;

namespace TaskManager.Domain.Entities;

public class TarefaAlteracao : ICriadoPor, ICriadoEm
{
    public int Id { get; init; }
    public Tarefa Tarefa { get; init; }
    public int TarefaId { get; init; }
    public string Titulo { get; init; }
    public string Descricao { get; init; }
    public LocalDate Vencimento { get; init; }
    public TarefaStatus Status { get; init; }
    public List<Comentario> Comentarios { get; init; }
    public int CriadoPorId { get; init; }
    public Usuario CriadoPor { get; init; }
    public Instant CriadoEm { get; init; }
}