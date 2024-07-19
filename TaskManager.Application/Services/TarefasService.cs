using Microsoft.EntityFrameworkCore;
using NodaTime;
using TaskManager.Data;
using TaskManager.Domain;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Services;

namespace TaskManager.Application.Services;

public class TarefasService : ITarefasService
{
    private readonly TaskManagerDbContext _dbContext;

    public TarefasService(TaskManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Tarefa> Obter(int id)
    {
        var tarefa = await _dbContext.Tarefas
            .Include(x => x.Comentarios)
            .Include(x => x.Alteracoes)
            .ThenInclude(x => x.Comentarios)
            .FirstOrDefaultAsync(x => x.Id == id);
        return tarefa;
    }

    public async Task<Result<Tarefa>> Criar(
        int projetoId,
        string titulo,
        string descricao,
        LocalDate vencimento,
        TarefaPrioridade prioridade)
    {
        var projeto = await _dbContext.Projetos
            .Include(x => x.Tarefas)
            .FirstOrDefaultAsync(x => x.Id == projetoId);
        if (projeto.Tarefas.Count == 20)
            return Result<Tarefa>.Failure("Projeto não pode ter mais que 20 tarefas.");
        var tarefa = new Tarefa(projetoId, titulo, descricao, vencimento, prioridade);
        _dbContext.Tarefas.Add(tarefa);
        await _dbContext.SaveChangesAsync();
        return Result<Tarefa>.Success(tarefa);
    }

    public async Task<Tarefa> Modificar(
        int tarefaId,
        string titulo,
        string descricao,
        LocalDate vencimento,
        TarefaStatus status)
    {
        var tarefa = await _dbContext.Tarefas
            .Include(x => x.Comentarios)
            .Include(x => x.Alteracoes)
            .ThenInclude(x => x.Comentarios)
            .FirstOrDefaultAsync(x => x.Id == tarefaId);
        tarefa.Modificar(
            titulo,
            descricao,
            vencimento,
            status);
        await _dbContext.SaveChangesAsync();
        return tarefa;
    }

    public async Task Remover(int tarefaId)
    {
        var tarefa = await _dbContext.Tarefas.FirstOrDefaultAsync(x => x.Id == tarefaId);
        _dbContext.Remove(tarefa);
        await _dbContext.SaveChangesAsync();
    }

    public async Task AdicionarComentario(int tarefaId, string texto)
    {
        var tarefa = await _dbContext.Tarefas
            .Include(x => x.Comentarios)
            .Include(x => x.Alteracoes)
            .ThenInclude(x => x.Comentarios)
            .FirstOrDefaultAsync(x => x.Id == tarefaId);
        tarefa.AdicionarComentario(texto);
        await _dbContext.SaveChangesAsync();
    }
}