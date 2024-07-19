using NodaTime;
using TaskManager.Domain.Common;

namespace TaskManager.Domain.Entities;

public class Comentario : ICriadoPor
{
    public int Id { get; init; }
    public string Texto { get; set; }
    public int CriadoPorId { get; init; }
    public Usuario CriadoPor { get; init; }
}