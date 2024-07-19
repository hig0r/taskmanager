using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Domain.Entities;

namespace TaskManager.Data.EntityConfigurations;

public class TarefaConfiguration : IEntityTypeConfiguration<Tarefa>
{
    public void Configure(EntityTypeBuilder<Tarefa> builder)
    {
        builder.HasMany(x => x.Alteracoes).WithOne(x => x.Tarefa);
        builder.HasOne(x => x.CriadoPor);
        builder.OwnsMany(x => x.Comentarios, x =>
        {
            x.HasOne(c => c.CriadoPor).WithMany();
            x.HasKey(c => c.Id);
        });
    }
}