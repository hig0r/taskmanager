namespace TaskManager.Domain.Entities;

public class TarefaAlteracaoComentario
{
    public int Id { get; init; }
    public string Texto { get; set; }
    public int CriadoPorId { get; init; }
    public Usuario CriadoPor { get; init; }
}