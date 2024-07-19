using TaskManager.Domain.Common;

namespace TaskManager.Domain.Entities;

public class Projeto : ICriadoPor
{
    // EF
    private Projeto()
    {
    }
    
    public Projeto(string nome)
    {
        Nome = nome;
    }

    public int Id { get; init; }
    public string Nome { get; set; }
    public ICollection<Tarefa> Tarefas { get; set; }
    public int CriadoPorId { get; init; }
    public Usuario CriadoPor { get; init; }
}