using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NodaTime;
using TaskManager.Domain.Common;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Services;

namespace TaskManager.Data;

public class TaskManagerDbContext : DbContext
{
    private readonly IUsuarioAutenticado _usuarioAutenticado;
    private readonly IClock _clock;

    public TaskManagerDbContext(
        DbContextOptions<TaskManagerDbContext> options,
        IUsuarioAutenticado usuarioAutenticado,
        IClock clock) : base(options)
    {
        _usuarioAutenticado = usuarioAutenticado;
        _clock = clock;
    }
    
    public DbSet<Projeto> Projetos { get; set; }
    public DbSet<Tarefa> Tarefas { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaskManagerDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = new())
    {
        var addedEntities = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added)
            .Select(e => e.Entity)
            .ToList();
        foreach (var entity in addedEntities)
        {
            if (entity is ICriadoEm e && e.CriadoEm == default)
                entity.GetType().GetProperty(nameof(ICriadoEm.CriadoEm))!.SetValue(entity, _clock.GetCurrentInstant());
            if (entity is ICriadoPor { CriadoPor: null, CriadoPorId: 0 })
            {
                var usuarioId = _usuarioAutenticado.UsuarioId;
                entity.GetType().GetProperty(nameof(ICriadoPor.CriadoPorId))!.SetValue(entity, usuarioId);
            }
        }
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
}