using Calendar;

public class IntervalTests
{
    [Theory]
    [InlineData("2024-01-01", "2024-01-10", 10)]
    [InlineData("2022-03-15", "2022-03-20", 6)]
    [InlineData("2023-05-01", "2023-05-05", 5)]
    public void CreateIntervalTest(string date1, string date2, int interval)
    {
        using TestFixture fixture = new TestFixture();
        Db_work db_Work = new Db_work(fixture.DbContext);

        var intervalObj = new Interval { date1 = DateTime.Parse(date1), date2 = DateTime.Parse(date2), interval = interval };
        db_Work.Create(intervalObj.date1, intervalObj.date2, intervalObj.interval);

        List<Interval> intervals = fixture.DbContext.Intervals.ToList();

        Assert.Single(intervals);
        Assert.Equal(intervalObj.date1, intervals[0].date1);
        Assert.Equal(intervalObj.date2, intervals[0].date2);
        Assert.Equal(intervalObj.interval, intervals[0].interval);
    }

    [Fact]
    public void CreateDuplicateIntervalTest()
    {
        using TestFixture fixture = new TestFixture();
        Db_work db_Work = new Db_work(fixture.DbContext);

        var intervalObj = new Interval { date1 = DateTime.Parse("2024-01-01"), date2 = DateTime.Parse("2024-01-10"), interval = 10 };
        db_Work.Create(intervalObj.date1, intervalObj.date2, intervalObj.interval);
        db_Work.Create(intervalObj.date1, intervalObj.date2, intervalObj.interval);

        List<Interval> intervals = fixture.DbContext.Intervals.ToList();

        Assert.Equal(2, intervals.Count);
        Assert.Equal(intervals[0].date1, intervals[1].date1);
        Assert.Equal(intervals[0].date2, intervals[1].date2);
        Assert.Equal(intervals[0].interval, intervals[1].interval);
    }

    [Theory]
    [InlineData("2024-01-01", "2024-01-10", 10)]
    [InlineData("2022-03-15", "2022-03-20", 6)]
    [InlineData("2023-05-01", "2023-05-05", 5)]
    public void ReadIntervalTest(string date1, string date2, int interval)
    {
        using TestFixture fixture = new TestFixture();
        Db_work db_Work = new Db_work(fixture.DbContext);

        var intervalObj = new Interval { date1 = DateTime.Parse(date1), date2 = DateTime.Parse(date2), interval = interval };
        db_Work.Create(intervalObj.date1, intervalObj.date2, intervalObj.interval);

        var interval2 = db_Work.Read(DateTime.Parse(date1), DateTime.Parse(date2));

        Assert.Equal(intervalObj.interval, interval2);
    }

    [Fact]
    public void ReadNonExistingIntervalTest()
    {
        using TestFixture fixture = new TestFixture();
        Db_work db_Work = new Db_work(fixture.DbContext);

        var interval = db_Work.Read(DateTime.Parse("2024-01-01"), DateTime.Parse("2024-01-10"));

        Assert.Null(interval);
    }

    [Fact]
    public void RemoveIntervalTest()
    {
        using TestFixture fixture = new TestFixture();
        Db_work db_Work = new Db_work(fixture.DbContext);

        var intervalObj = new Interval { date1 = DateTime.Parse("2024-01-01"), date2 = DateTime.Parse("2024-01-10"), interval = 10 };
        db_Work.Create(intervalObj.date1, intervalObj.date2, intervalObj.interval);
        db_Work.Remove(DateTime.Parse("2024-01-01"), DateTime.Parse("2024-01-10"));

        Assert.Empty(fixture.DbContext.Intervals);
    }

    [Fact]
    public void RemoveMultipleIntervalsTest()
    {
        using TestFixture fixture = new TestFixture();
        Db_work db_Work = new Db_work(fixture.DbContext);

        db_Work.Create(DateTime.Parse("2023-01-01"), DateTime.Parse("2023-01-10"), 10);
        db_Work.Create(DateTime.Parse("2023-01-01"), DateTime.Parse("2023-01-10"), 20);
        db_Work.Create(DateTime.Parse("2023-01-01"), DateTime.Parse("2023-01-10"), 30);

        db_Work.Remove(DateTime.Parse("2023-01-01"), DateTime.Parse("2023-01-10"));

        Assert.Empty(fixture.DbContext.Intervals);
    }

    [Fact]
    public void RemoveNotEverythingIntervalsTest()
    {
        using TestFixture fixture = new TestFixture();
        Db_work db_Work = new Db_work(fixture.DbContext);

        db_Work.Create(DateTime.Parse("2023-01-01"), DateTime.Parse("2023-01-10"), 10);
        db_Work.Create(DateTime.Parse("2023-02-01"), DateTime.Parse("2023-02-10"), 20);
        db_Work.Create(DateTime.Parse("2023-03-01"), DateTime.Parse("2023-03-10"), 30);

        db_Work.Remove(DateTime.Parse("2023-02-01"), DateTime.Parse("2023-02-10"));

        Assert.Equal(2, fixture.DbContext.Intervals.Count());
    }

    [Fact]
    public void RemoveNonExistingIntervalTest()
    {
        using TestFixture fixture = new TestFixture();
        Db_work db_Work = new Db_work(fixture.DbContext);

        db_Work.Create(DateTime.Parse("2023-01-01"), DateTime.Parse("2023-01-10"), 10);
        db_Work.Create(DateTime.Parse("2023-02-01"), DateTime.Parse("2023-02-10"), 20);
        db_Work.Create(DateTime.Parse("2023-03-01"), DateTime.Parse("2023-03-10"), 30);

        db_Work.Remove(DateTime.Parse("2025-01-01"), DateTime.Parse("2025-01-10"));

        Assert.Equal(3, fixture.DbContext.Intervals.Count());
    }
}
