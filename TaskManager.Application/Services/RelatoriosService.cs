using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Dtos;
using TaskManager.Data;

namespace TaskManager.Application.Services;

public class RelatoriosService
{
    private readonly TaskManagerDbContext _dbContext;

    public RelatoriosService(TaskManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<RelatorioMediaTarefasConcluidas[]> GerarRelatorio()
    {
        var conn = _dbContext.Database.GetDbConnection();
        var sql = """
                  SELECT U.*, COUNT(0) AS TarefasConcluidas FROM "TarefaAlteracao" TA
                  JOIN public."Tarefas" T on TA."TarefaId" = T."Id"
                  JOIN public."Usuarios" U on T."CriadoPorId" = U."Id"
                  WHERE TA."Status" = 2 AND TA."CriadoEm" > DATE_TRUNC('day', NOW() - INTERVAL '30 days')
                  GROUP BY U."Id", U."Nome"
                  """;
        var result = await conn.QueryAsync<RelatorioMediaTarefasConcluidas>(sql);
        return result.ToArray();
    }
}