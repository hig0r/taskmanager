using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Domain;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Services;

namespace TaskManager.Application.Services;

public class ProjetosService : IProjetosService
{
    private readonly TaskManagerDbContext _dbContext;
    private readonly IUsuarioAutenticado _usuarioAutenticado;

    public ProjetosService(TaskManagerDbContext dbContext, IUsuarioAutenticado usuarioAutenticado)
    {
        _dbContext = dbContext;
        _usuarioAutenticado = usuarioAutenticado;
    }

    public Task<Projeto?> ObterPorId(int id)
    {
        return _dbContext.Projetos
            .Include(x => x.Tarefas)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<Projeto[]> Listar()
    {
        return _dbContext.Projetos
            .AsNoTracking()
            .Where(x => x.CriadoPorId == _usuarioAutenticado.UsuarioId)
            .ToArrayAsync();
    }

    public async Task<Projeto> Criar(string nome)
    {
        var projeto = new Projeto(nome);
        _dbContext.Projetos.Add(projeto);
        await _dbContext.SaveChangesAsync();
        return projeto;
    }

    public async Task<Result> Remover(int projetoId)
    {
        var projeto = await _dbContext.Projetos
            .Include(x => x.Tarefas)
            .FirstOrDefaultAsync(x => x.Id == projetoId);
        if (projeto == null)
            return Result.Failure(new NotFound());
        if (projeto.Tarefas.Any(x => x.Status != TarefaStatus.Concluida))
            return Result.Failure("O projeto não pode ser deletado, pois ainda possui tarefas pendentes.");
        _dbContext.Remove(projeto);
        await _dbContext.SaveChangesAsync();
        return Result.Success();
    }
}