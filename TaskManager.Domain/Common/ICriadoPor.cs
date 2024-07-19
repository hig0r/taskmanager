using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Common;

public interface ICriadoPor
{
    public int CriadoPorId { get; init; }
    public Usuario CriadoPor { get; init; }
}