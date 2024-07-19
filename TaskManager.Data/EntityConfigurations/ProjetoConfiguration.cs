using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Domain.Entities;

namespace TaskManager.Data.EntityConfigurations;

public class ProjetoConfiguration : IEntityTypeConfiguration<Projeto>
{
    public void Configure(EntityTypeBuilder<Projeto> builder)
    {
        builder.HasMany(x => x.Tarefas).WithOne(x => x.Projeto);
        builder.HasOne(x => x.CriadoPor).WithMany();
    }
}