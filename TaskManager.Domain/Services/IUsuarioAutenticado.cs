using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Services;

public interface IUsuarioAutenticado
{
    public int UsuarioId { get; }
    public UsuarioFuncao Funcao { get; }
}