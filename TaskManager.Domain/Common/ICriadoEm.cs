using NodaTime;

namespace TaskManager.Domain.Common;

public interface ICriadoEm
{
    public Instant CriadoEm { get; init; }
}