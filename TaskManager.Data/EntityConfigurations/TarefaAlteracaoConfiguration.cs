using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Domain.Entities;

namespace TaskManager.Data.EntityConfigurations;

public class TarefaAlteracaoConfiguration : IEntityTypeConfiguration<TarefaAlteracao>
{
    public void Configure(EntityTypeBuilder<TarefaAlteracao> builder)
    {
        builder.OwnsMany(x => x.Comentarios, x =>
        {
            x.HasOne(c => c.CriadoPor).WithMany();
            x.HasKey(c => c.Id);
        });
    }
}