using Microsoft.EntityFrameworkCore;

namespace Calendar;

public class CalendarDb : DbContext
{
    public DbSet<Leapnes> Leapness { get; set; }
    public DbSet<Interval> Intervals { get; set; }
    public DbSet<DayWeek> DayOfWeeks { get; set; }

    public CalendarDb(DbContextOptions<CalendarDb> options) : base(options) { }

    public CalendarDb() : base(new DbContextOptionsBuilder<CalendarDb>().UseSqlite($"Data Source={GetPath()}").Options) { }

    public static String GetPath()
    {
        return "../db/calendar.db";
    }
}

[PrimaryKey(nameof(Leapnes_id))]
public class Leapnes
{
    public int Leapnes_id { get; set; }
    public int year { get; set; }
    public bool leap { get; set; }
}
[PrimaryKey(nameof(Id))]
public class LeapnesRequest
{
    public int Id { get; set; }
    public int year { get; set; }
}

[PrimaryKey(nameof(interval_id))]
public class Interval
{
    public int interval_id { get; set; }
    public DateTime date1 { get; set; }
    public DateTime date2 { get; set; }
    public int interval { get; set; }
}

[PrimaryKey(nameof(Id))]
public class IntervalRequest
{
    public int Id { get; set; }
    public DateTime date1 { get; set; }
    public DateTime date2 { get; set; }
}

[PrimaryKey(nameof(DayWeekId))]
public class DayWeek
{
    public int DayWeekId { get; set; }
    public DateTime date { get; set; }
    public int dayWeek { get; set; }
}

[PrimaryKey(nameof(Id))]
public class DayWeekRequest
{
    public int Id { get; set; }
    public DateTime date { get; set; }
}