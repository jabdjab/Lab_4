using Calendar;

public class DayWeekTests
{
    [Theory]
    [InlineData("2024-01-01", 4)]
    [InlineData("2022-03-15", 2)]
    [InlineData("2023-05-01", 1)]
    public void CreateDayWeekTest(string date, int dayOfWeek)
    {
        using TestFixture fixture = new TestFixture();
        Db_work db_Work = new Db_work(fixture.DbContext);

        var dayWeekObj = new DayWeek { date = DateTime.Parse(date), dayWeek = dayOfWeek };
        db_Work.Create(dayWeekObj.date, dayWeekObj.dayWeek);

        List<DayWeek> dayWeeks = fixture.DbContext.DayOfWeeks.ToList();

        Assert.Single(dayWeeks);
        Assert.Equal(dayWeekObj.date, dayWeeks[0].date);
        Assert.Equal(dayWeekObj.dayWeek, dayWeeks[0].dayWeek);
    }

    [Fact]
    public void CreateDuplicateDayWeekTest()
    {
        using TestFixture fixture = new TestFixture();
        Db_work db_Work = new Db_work(fixture.DbContext);

        var dayWeekObj = new DayWeek { date = DateTime.Parse("2024-01-01"), dayWeek = 4 }; // Thursday
        db_Work.Create(dayWeekObj.date, dayWeekObj.dayWeek);
        db_Work.Create(dayWeekObj.date, dayWeekObj.dayWeek);

        List<DayWeek> dayWeeks = fixture.DbContext.DayOfWeeks.ToList();

        Assert.Equal(2, dayWeeks.Count);
        Assert.Equal(dayWeeks[0].date, dayWeeks[1].date);
        Assert.Equal(dayWeeks[0].dayWeek, dayWeeks[1].dayWeek);
    }

    [Theory]
    [InlineData("2024-01-01", 4)] // Thursday
    [InlineData("2022-03-15", 2)] // Tuesday
    [InlineData("2023-05-01", 1)] // Monday
    public void ReadDayWeekTest(string date, int dayOfWeek)
    {
        using TestFixture fixture = new TestFixture();
        Db_work db_Work = new Db_work(fixture.DbContext);

        var dayWeekObj = new DayWeek { date = DateTime.Parse(date), dayWeek = dayOfWeek };
        db_Work.Create(dayWeekObj.date, dayWeekObj.dayWeek);

        var dayWeek = db_Work.Read(DateTime.Parse(date));

        Assert.Equal(dayWeekObj.dayWeek, dayWeek);
    }

    [Fact]
    public void ReadNonExistingDayWeekTest()
    {
        using TestFixture fixture = new TestFixture();
        Db_work db_Work = new Db_work(fixture.DbContext);

        var dayWeek = db_Work.Read(DateTime.Parse("2024-01-01"));

        Assert.Null(dayWeek);
    }

    [Fact]
    public void RemoveDayWeekTest()
    {
        using TestFixture fixture = new TestFixture();
        Db_work db_Work = new Db_work(fixture.DbContext);

        var dayWeekObj = new DayWeek { date = DateTime.Parse("2024-01-01"), dayWeek = 4 }; // Thursday
        db_Work.Create(dayWeekObj.date, dayWeekObj.dayWeek);
        db_Work.Remove(DateTime.Parse("2024-01-01"));

        Assert.Empty(fixture.DbContext.DayOfWeeks);
    }

    [Fact]
    public void RemoveMultipleDayWeekTest()
    {
        using TestFixture fixture = new TestFixture();
        Db_work db_Work = new Db_work(fixture.DbContext);

        db_Work.Create(DateTime.Parse("2023-01-01"), 1);
        db_Work.Create(DateTime.Parse("2023-01-01"), 2);
        db_Work.Create(DateTime.Parse("2023-01-01"), 3);

        db_Work.Remove(DateTime.Parse("2023-01-01"));

        Assert.Empty(fixture.DbContext.DayOfWeeks);
    }

    [Fact]
    public void RemoveNotEverythingDayWeekTest()
    {
        using TestFixture fixture = new TestFixture();
        Db_work db_Work = new Db_work(fixture.DbContext);

        db_Work.Create(DateTime.Parse("2023-01-01"), 1);
        db_Work.Create(DateTime.Parse("2023-02-01"), 2);
        db_Work.Create(DateTime.Parse("2023-03-01"), 3);

        db_Work.Remove(DateTime.Parse("2023-02-01"));

        Assert.Equal(2, fixture.DbContext.DayOfWeeks.Count());
    }


    [Fact]
    public void RemoveNonExistingDayWeekTest()
    {
        using TestFixture fixture = new TestFixture();
        Db_work db_Work = new Db_work(fixture.DbContext);

        db_Work.Create(DateTime.Parse("2023-01-01"), 1);
        db_Work.Create(DateTime.Parse("2023-02-01"), 2);
        db_Work.Create(DateTime.Parse("2023-03-01"), 3);

        db_Work.Remove(DateTime.Parse("2025-01-01"));

        Assert.Equal(3, fixture.DbContext.DayOfWeeks.Count());
    }
}
