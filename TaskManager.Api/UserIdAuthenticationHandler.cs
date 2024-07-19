using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TaskManager.Data;

namespace TaskManager.Api;

public class UserIdAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly TaskManagerDbContext _dbContext;

    public UserIdAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        TaskManagerDbContext dbContext) 
        : base(options, logger, encoder, clock)
    {
        _dbContext = dbContext;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue("userid", out var userIdHeaders))
            return AuthenticateResult.Fail("");
        
        var userIdStr = userIdHeaders.FirstOrDefault();
        
        if (string.IsNullOrEmpty(userIdStr))
            return AuthenticateResult.Fail("");

        var valid = int.TryParse(userIdStr, out var userId);
        
        if (!valid) return AuthenticateResult.Fail("");

        var user = await _dbContext.Usuarios.FirstOrDefaultAsync(x => x.Id == userId);
        
        if (user == null) return AuthenticateResult.Fail("");

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Funcao.ToString())
        };

        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return AuthenticateResult.Success(ticket);
    }
}