using Calendar;

public class LeapnessTests
{
    [Theory]
    [InlineData(2000, false)]
    [InlineData(1700, true)]
    [InlineData(2024, true)]
    [InlineData(2021, false)]
    public void CreateLeapnessLeapnessTest(int year, bool leapness)
    {
        using TestFixture fixture = new TestFixture();
        Db_work db_Work = new Db_work(fixture.DbContext);

        db_Work.Create(leapness, year);
        List<Leapnes> leapnes = fixture.DbContext.Leapness.ToList();

        Assert.Single(leapnes);
        Assert.Equal(year, leapnes[0].year);
        Assert.Equal(leapness, leapnes[0].leap);
    }

    public void CreateDoubleLeapnessTest()
    {
        using TestFixture fixture = new TestFixture();
        Db_work db_Work = new Db_work(fixture.DbContext);

        db_Work.Create(false, 1719);
        db_Work.Create(false, 1719);

        List<Leapnes> leapnes = fixture.DbContext.Leapness.ToList();

        Assert.Equal(2, leapnes.Count);
        Assert.Equal(leapnes[0].year, leapnes[1].year);
        Assert.Equal(leapnes[0].leap, leapnes[1].leap);
    }

    [Theory]
    [InlineData(2000, false)]
    [InlineData(1800, true)]
    [InlineData(2024, true)]
    [InlineData(2021, false)]
    public void ReadLeapnessLeapnessTest(int year, bool leapness)
    {
        using TestFixture fixture = new TestFixture();
        Db_work db_Work = new Db_work(fixture.DbContext);
        db_Work.Create(leapness, year);
        bool? leapness2 = db_Work.Read(year);
        Assert.Equal(leapness, leapness2);
    }

    [Fact]
    public void ReadNonExistingLeapnessTest()
    {
        using TestFixture fixture = new TestFixture();
        Db_work db_Work = new Db_work(fixture.DbContext);
        bool? leapness = db_Work.Read(1800);
        Assert.Null(leapness);
    }

    [Fact]
    public void RemoveOneLeapnessTest()
    {
        using TestFixture fixture = new TestFixture();
        Db_work db_Work = new Db_work(fixture.DbContext);
        db_Work.Create(false, 2023);
        db_Work.Remove(2023);
        Assert.Empty(fixture.DbContext.Leapness);
    }

    [Fact]
    public void RemoveMultipleLeapnessTest()
    {
        using TestFixture fixture = new TestFixture();
        Db_work db_Work = new Db_work(fixture.DbContext);
        db_Work.Create(false, 2023);
        db_Work.Create(true, 2023);
        db_Work.Create(false, 2023);
        db_Work.Remove(2023);
        Assert.Empty(fixture.DbContext.Leapness);
    }

    [Fact]
    public void RemoveNotEverythingLeapnessTest()
    {
        using TestFixture fixture = new TestFixture();
        Db_work db_Work = new Db_work(fixture.DbContext);
        db_Work.Create(false, 2022);
        db_Work.Create(true, 2023);
        db_Work.Create(false, 2023);
        db_Work.Remove(2023);
        Assert.Single(fixture.DbContext.Leapness);
        Assert.Equal(2022, fixture.DbContext.Leapness.First().year);
        Assert.Equal(false, fixture.DbContext.Leapness.First().leap);
    }

    [Fact]
    public void RemoveNonExistingLeapnessTest()
    {
        using TestFixture fixture = new TestFixture();
        Db_work db_Work = new Db_work(fixture.DbContext);

        db_Work.Create(false, 2022);
        db_Work.Create(true, 2023);
        db_Work.Create(false, 2023);
        db_Work.Remove(2024);

        Assert.Equal(3, fixture.DbContext.Leapness.Count());
    }
}