using System.Security.Claims;
using TaskManager.Domain.Services;

namespace TaskManager.Api.Services;

public class UsuarioAutenticado : IUsuarioAutenticado
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UsuarioAutenticado(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public int UsuarioId => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
}