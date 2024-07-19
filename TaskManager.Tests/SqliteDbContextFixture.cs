using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using NodaTime;
using TaskManager.Data;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Services;

namespace TaskManager.Tests;

public class SqliteDbContextFixture : IDisposable
{
    public TaskManagerDbContext DbContext { get; }
    
    public SqliteDbContextFixture(Mock<IUsuarioAutenticado>? mockUsuarioAutenticado = null)
    {
        if (mockUsuarioAutenticado == null)
        {
            mockUsuarioAutenticado = new Mock<IUsuarioAutenticado>();
            mockUsuarioAutenticado.Setup(x => x.UsuarioId).Returns(1);
        }

        var mockClock = new Mock<IClock>();
        mockClock.Setup(x => x.GetCurrentInstant()).Returns(SystemClock.Instance.GetCurrentInstant());
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var dbContextOptions = new DbContextOptionsBuilder<TaskManagerDbContext>()
            .UseSqlite(connection, x => x.UseNodaTime())
            .Options;
        DbContext = new TaskManagerDbContext(dbContextOptions, mockUsuarioAutenticado.Object, mockClock.Object);
        DbContext.Database.EnsureCreated();
        DbContext.SaveChanges();
    }

    public void Dispose()
    {
        DbContext.Dispose();
    }
}