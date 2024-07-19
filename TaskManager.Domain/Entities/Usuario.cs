namespace TaskManager.Domain.Entities;

public class Usuario
{
    public int Id { get; init; }
    public string Nome { get; set; }
    public UsuarioFuncao Funcao { get; set; }
}