using NodaTime;
using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Services;

public interface ITarefasService
{
    Task<Tarefa> Obter(int id);
    
    Task<Result<Tarefa>> Criar(
        int projetoId,
        string titulo,
        string descricao,
        LocalDate vencimento,
        TarefaPrioridade prioridade);

    Task<Tarefa> Modificar(
        int tarefaId,
        string titulo,
        string descricao,
        LocalDate vencimento,
        TarefaStatus status);

    Task Remover(int tarefaId);

    Task AdicionarComentario(int tarefaId, string texto);
}