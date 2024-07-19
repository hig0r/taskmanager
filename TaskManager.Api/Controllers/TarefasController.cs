using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Dtos;
using TaskManager.Domain.Services;

namespace TaskManager.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TarefasController : ControllerBase
{
    private readonly ITarefasService _tarefasService;

    public TarefasController(ITarefasService tarefasService)
    {
        _tarefasService = tarefasService;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var tarefa = await _tarefasService.Obter(id);
        return Ok(tarefa);
    }
    
    [HttpPost]
    public async Task<IActionResult> Post(CriarTarefaDto dto)
    {
        var result = await _tarefasService.Criar(
            dto.ProjetoId,
            dto.Titulo,
            dto.Descricao,
            dto.Vencimento,
            dto.Prioridade);
        if (result.IsFailure)
            return BadRequest(result.Error.Description);
        return CreatedAtAction(nameof(GetById), new { result.Value.Id }, result.Value);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, AtualizarTarefaDto dto)
    {
        var result = await _tarefasService.Modificar(
            id,
            dto.Titulo,
            dto.Descricao,
            dto.Vencimento,
            dto.Status);
        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _tarefasService.Remover(id);
        return NoContent();
    }

    [HttpPost("{tarefaId:int}/comentario")]
    public async Task<IActionResult> PostComment(int tarefaId, CriarComentarioDto dto)
    {
        await _tarefasService.AdicionarComentario(tarefaId, dto.Texto);
        return NoContent();
    }
}