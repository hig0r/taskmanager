using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Domain.Entities;

namespace TaskManager.Data.EntityConfigurations;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.HasData(
            new Usuario
            {
                Id = 1,
                Nome = "João",
                Funcao = UsuarioFuncao.Nenhuma
            },
            new Usuario
            {
                Id = 2,
                Nome = "Maria",
                Funcao = UsuarioFuncao.Gerente
            });
    }
}