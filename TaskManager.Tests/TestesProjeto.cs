using FluentAssertions;
using Moq;
using NodaTime;
using TaskManager.Application;
using TaskManager.Application.Services;
using TaskManager.Data;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Services;

namespace TaskManager.Tests;

public class TestesProjeto
{
    [Fact]
    public async Task Deletar_ProjetoComTarefasPendentes_DeveRetornarErro()
    {
        using var dbContextFixture = new SqliteDbContextFixture();
        var dbContext = dbContextFixture.DbContext;
        var projetosService = new ProjetosService(dbContext, new Mock<IUsuarioAutenticado>().Object);

        dbContext.Projetos.Add(new Projeto("Test")
        {
            Id = 1,
            Tarefas = new List<Tarefa>
            {
                new(1, "Test", "Test", LocalDate.MinIsoValue, TarefaPrioridade.Baixa)
            }
        });
        await dbContext.SaveChangesAsync();

        var result = await projetosService.Remover(1);

        result.IsFailure.Should().BeTrue();
        result.Error.Description.Should().Be("O projeto não pode ser deletado, pois ainda possui tarefas pendentes.");
    }

    [Fact]
    public async Task Buscar_Projetos_DeveRetornarApenasProjetosDoUsuarioAutenticado()
    {
        var mockUsuarioAutenticado = new Mock<IUsuarioAutenticado>();
        mockUsuarioAutenticado.Setup(x => x.UsuarioId).Returns(1);
        using var dbContextFixture = new SqliteDbContextFixture();
        var dbContext = dbContextFixture.DbContext;
        dbContext.Projetos.AddRange(new Projeto("")
        {
            Id = 1,
            CriadoPorId = 1
        },
        new Projeto("")
        {
            Id = 2,
            CriadoPorId = 2
        });
        await dbContext.SaveChangesAsync();
        var projetosService = new ProjetosService(dbContext, mockUsuarioAutenticado.Object);

        var result = await projetosService.Listar();

        result.Should().HaveCount(1);
        result.First().CriadoPorId.Should().Be(1);
    }

    [Fact]
    public async Task Deletar_ProjetoSemTarefasPendentes_DeveFuncionar()
    {
        using var dbContextFixture = new SqliteDbContextFixture();
        var dbContext = dbContextFixture.DbContext;
        var projetosService = new ProjetosService(dbContext, new Mock<IUsuarioAutenticado>().Object);

        var tarefa = new Tarefa(1, "Test", "Test", LocalDate.MinIsoValue, TarefaPrioridade.Baixa);
        tarefa.Modificar(tarefa.Titulo, tarefa.Descricao, tarefa.Vencimento, TarefaStatus.Concluida);
        dbContext.Projetos.Add(new Projeto("Test")
        {
            Id = 1,
            Tarefas = new List<Tarefa>
            {
                tarefa
            }
        });
        await dbContext.SaveChangesAsync();

        var result = await projetosService.Remover(1);

        result.IsSuccess.Should().BeTrue();
    }
}