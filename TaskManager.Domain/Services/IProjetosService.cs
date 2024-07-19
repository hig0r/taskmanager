using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Services;

public interface IProjetosService
{
    Task<Projeto?> ObterPorId(int id);
    Task<Projeto[]> Listar();
    Task<Projeto> Criar(string nome);
    Task<Result> Remover(int projetoId);
}