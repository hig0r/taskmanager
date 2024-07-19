using System.Collections.Immutable;
using Mapster;
using NodaTime;
using TaskManager.Domain.Common;

namespace TaskManager.Domain.Entities;

public class Tarefa : ICriadoPor, ICriadoEm
{
    // EF
    private Tarefa()
    {
    }

    public Tarefa(
        int projetoId,
        string titulo,
        string descricao,
        LocalDate vencimento,
        TarefaPrioridade prioridade)
    {
        ProjetoId = projetoId;
        Titulo = titulo;
        Descricao = descricao;
        Vencimento = vencimento;
        Prioridade = prioridade;
    }

    public int Id { get; init; }
    public int ProjetoId { get; private init; }
    public Projeto Projeto { get; private init; }
    public string Titulo { get; private set; }
    public string Descricao { get; private set; }
    public LocalDate Vencimento { get; private set; }
    public TarefaStatus Status { get; private set; } = TarefaStatus.Pendente;
    public TarefaPrioridade Prioridade { get; private init; }
    private List<TarefaAlteracao> _alteracoes = [];
    public IReadOnlyCollection<TarefaAlteracao> Alteracoes => _alteracoes;
    private List<Comentario> _comentarios = [];
    public IReadOnlyCollection<Comentario> Comentarios => _comentarios;

    public int CriadoPorId { get; init; }
    public Usuario CriadoPor { get; init; }

    public Instant CriadoEm { get; init; }

    public void AdicionarAlteracao()
    {
        _alteracoes.Add(new TarefaAlteracao
        {
            Titulo = Titulo,
            Descricao = Descricao,
            TarefaId = Id,
            Comentarios = _comentarios
                .Select(x =>
                    new Comentario
                    {
                        Texto = x.Texto,
                        CriadoPorId = x.CriadoPorId
                    }
                )
                .ToList(),
            Status = Status,
            Vencimento = Vencimento
        });
    }

    public void Modificar(
        string titulo,
        string descricao,
        LocalDate vencimento,
        TarefaStatus status)
    {
        AdicionarAlteracao();
        Titulo = titulo;
        Descricao = descricao;
        Vencimento = vencimento;
        Status = status;
    }

    public void AdicionarComentario(string texto)
    {
        AdicionarAlteracao();
        _comentarios.Add(new Comentario
        {
            Texto = texto,
        });
    }
}