using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Dtos;
using TaskManager.Domain.Services;

namespace TaskManager.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class ProjetosController : ControllerBase
{
    private readonly IProjetosService _projetosService;

    public ProjetosController(IProjetosService projetosService)
    {
        _projetosService = projetosService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var projetos = await _projetosService.Listar();
        return Ok(projetos);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var projeto = await _projetosService.ObterPorId(id);
        if (projeto == null) return NotFound();
        return Ok(projeto);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CriarProjetoDto dto)
    {
        var projeto = await _projetosService.Criar(dto.Nome);
        return CreatedAtAction(nameof(GetById), new { projeto.Id }, projeto);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _projetosService.Remover(id);
        if (result.IsFailure)
            return BadRequest(result.Error.Description);
        return NoContent();
    }
}