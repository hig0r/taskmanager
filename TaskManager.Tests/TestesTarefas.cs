using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using TaskManager.Application;
using TaskManager.Application.Services;
using TaskManager.Domain.Entities;

namespace TaskManager.Tests;

public class TestesTarefas
{
    [Fact]
    public async Task Modificar_Tarefa_DeveSalvarNoHistorico()
    {
        using var dbContextFixture = new SqliteDbContextFixture();
        var dbContext = dbContextFixture.DbContext;
        var tarefasService = new TarefasService(dbContext);
        var tarefa = new Tarefa(1, "t1", "d1", LocalDate.MinIsoValue, TarefaPrioridade.Baixa)
        {
            Id = 1
        };
        dbContext.Projetos.Add(new Projeto("Test")
        {
            Id = 1,
            Tarefas = new List<Tarefa>
            {
                tarefa
            }
        });
        await dbContext.SaveChangesAsync();

        await tarefasService.Modificar(tarefa.Id, "t2", "d2", LocalDate.MinIsoValue, TarefaStatus.EmAndamento);
        var tarefaTest = dbContext.Tarefas
            .Include(x => x.Alteracoes)
            .First(x => x.Id == tarefa.Id);

        tarefaTest.Titulo.Should().Be("t2");
        tarefaTest.Descricao.Should().Be("d2");
        tarefaTest.Status.Should().Be(TarefaStatus.EmAndamento);
        tarefaTest.Alteracoes.Should().HaveCount(1);
        tarefaTest.Alteracoes.First().Titulo.Should().Be("t1");
        tarefaTest.Alteracoes.First().Descricao.Should().Be("d1");
        tarefaTest.Alteracoes.First().Status.Should().Be(TarefaStatus.Pendente);
    }
    
    [Fact]
    public async Task Adicionar_Comentario_DeveSalvarNoHistorico()
    {
        using var dbContextFixture = new SqliteDbContextFixture();
        var dbContext = dbContextFixture.DbContext;
        var tarefasService = new TarefasService(dbContext);
        var tarefa = new Tarefa(1, "t1", "d1", LocalDate.MinIsoValue, TarefaPrioridade.Baixa)
        {
            Id = 1
        };
        dbContext.Projetos.Add(new Projeto("Test")
        {
            Id = 1,
            Tarefas = new List<Tarefa>
            {
                tarefa
            }
        });
        await dbContext.SaveChangesAsync();

        await tarefasService.AdicionarComentario(tarefa.Id, "c");
        var tarefaTest = dbContext.Tarefas
            .Include(x => x.Alteracoes)
            .ThenInclude(x => x.Comentarios)
            .Include(x => x.Comentarios)
            .First(x => x.Id == tarefa.Id);

        tarefaTest.Comentarios.Should().HaveCount(1);
        tarefaTest.Comentarios.First().Texto.Should().Be("c");
        tarefaTest.Alteracoes.Should().HaveCount(1);
        tarefaTest.Alteracoes.First().Comentarios.Should().BeEmpty();
    }
    
    [Fact]
    public async Task Adicionar_MaisQue20TarefasNoMesmoProjeto_DeveRetornarErro()
    {
        using var dbContextFixture = new SqliteDbContextFixture();
        var dbContext = dbContextFixture.DbContext;
        var tarefasService = new TarefasService(dbContext);
        var tarefas = Enumerable.Range(1, 20).Select(i =>
            new Tarefa(1, "t", "d", LocalDate.MinIsoValue, TarefaPrioridade.Baixa)
            {
                Id = i
            });
        dbContext.Projetos.Add(new Projeto("Test")
        {
            Id = 1,
            Tarefas = tarefas.ToList()
        });
        await dbContext.SaveChangesAsync();

        var result = await tarefasService.Criar(1, "", "", LocalDate.MinIsoValue, TarefaPrioridade.Alta);

        result.IsFailure.Should().BeTrue();
        result.Error.Description.Should().Be("Projeto não pode ter mais que 20 tarefas.");
    }
}