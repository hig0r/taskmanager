using TaskManager.Domain.Entities;
using TaskManager.Domain.Services;

namespace TaskManager.Api.Services;

public class UsuarioAutenticado : IUsuarioAutenticado
{
    public int UsuarioId => 1;
    public UsuarioFuncao Funcao => UsuarioFuncao.Nenhuma;
}