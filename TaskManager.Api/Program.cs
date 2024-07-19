using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NodaTime;
using NodaTime.Serialization.SystemTextJson;
using Npgsql;
using TaskManager.Api;
using TaskManager.Api.Services;
using TaskManager.Application.Services;
using TaskManager.Data;
using TaskManager.Domain.Services;
using SystemClock = NodaTime.SystemClock;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddControllers()
    .AddJsonOptions(x =>
    {
        x.JsonSerializerOptions.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);
        x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        x.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });;
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("userid", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "userid",
        Type = SecuritySchemeType.ApiKey,
        Description = "User ID for custom authentication",
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "userid"
                }
            },
            Array.Empty<string>()
        }
    });
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUsuarioAutenticado, UsuarioAutenticado>();
builder.Services.AddScoped<ITarefasService, TarefasService>();
builder.Services.AddScoped<IProjetosService, ProjetosService>();
builder.Services.AddScoped<RelatoriosService>();
builder.Services.AddSingleton<IClock>(SystemClock.Instance);
builder.Services.AddAuthentication("UserIdScheme")
    .AddScheme<AuthenticationSchemeOptions, UserIdAuthenticationHandler>("UserIdScheme", options => { });
var dataSourceBuilder = new NpgsqlDataSourceBuilder(builder.Configuration.GetConnectionString("Database")!);
dataSourceBuilder.UseNodaTime();
var dataSource = dataSourceBuilder.Build();
builder.Services.AddDbContext<TaskManagerDbContext>(options => 
    options.UseNpgsql(dataSource, x => x.UseNodaTime()));

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

if (app.Environment.IsProduction())
{
    var scope = app.Services.CreateScope();
    scope.ServiceProvider.GetRequiredService<TaskManagerDbContext>().Database.Migrate();
}

app.Run();