using Calendar;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

public class TestFixture : IDisposable
{
    public CalendarDb DbContext { get; private set; }

    public TestFixture()
    {
        var options = new DbContextOptionsBuilder<CalendarDb>()
            .EnableSensitiveDataLogging()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;

        DbContext = new CalendarDb(options);
        DbContext.Database.EnsureDeleted();
        DbContext.Database.EnsureCreated();
    }

    public void Dispose()
    {
        DbContext.Dispose();
    }
}
