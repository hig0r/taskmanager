using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Services;

namespace TaskManager.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class RelatoriosController : ControllerBase
{
    private readonly RelatoriosService _relatoriosService;

    public RelatoriosController(RelatoriosService relatoriosService)
    {
        _relatoriosService = relatoriosService;
    }
    
    [HttpGet]
    [Authorize(Roles = "Gerente")]
    public async Task<IActionResult> Get()
    {
        return Ok(await _relatoriosService.GerarRelatorio());
    }
}