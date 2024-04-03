namespace Calendar;

public class Db_work
{
    public CalendarDb db;

    public Db_work(CalendarDb db_)
    {
        db = db_;
    }
    
    public int Create(bool leap_, int year_)
    {
        Leapnes leapness = new Leapnes { leap = leap_, year = year_ };
        db.Add(leapness);
        db.SaveChanges();
        return leapness.Leapnes_id;
    }

    public bool? Read(int year_)
    {
        return db.Leapness.FirstOrDefault(b => b.year == year_)?.leap;
    }

    public void Remove(int year_)
    {
        var entries = db.Leapness.Where(b => b.year == year_).ToList();
        for (int i = 0; i < entries.Count; ++i)
        {
            db.Remove(entries[i]);
        }
        db.SaveChanges();
    }

    public int Create(DateTime date1_, DateTime date2_, int interval_)
    {
        Interval interval = new Interval { date1 = date1_, date2 = date2_, interval = interval_ };
        db.Add(interval);
        db.SaveChanges();
        return interval.interval_id;
    }

    public int? Read(DateTime date1_, DateTime date2_)
    {
        return db.Intervals.FirstOrDefault(b => b.date1 == date1_ && b.date2 == date2_)?.interval;
    }

    public void Remove(DateTime date1_, DateTime date2_)
    {
        var entries = db.Intervals.Where(b => b.date1 == date1_ && b.date2 == date2_).ToList();
        for (int i = 0; i < entries.Count; ++i)
        {
            db.Remove(entries[i]);
        }
        db.SaveChanges();
    }

    public int Create(DateTime date_, int dayWeek_)
    {
        DayWeek dayWeek = new DayWeek { date = date_, dayWeek = dayWeek_ };
        db.Add(dayWeek);
        db.SaveChanges();
        return dayWeek.DayWeekId;
    }

    public int? Read(DateTime date_)
    {
        return db.DayOfWeeks.FirstOrDefault(b => b.date == date_)?.dayWeek;
    }

    public void Remove(DateTime date_)
    {
        var entries = db.DayOfWeeks.Where(b => b.date == date_).ToList();
        for (int i = 0; i < entries.Count; ++i)
        {
            db.Remove(entries[i]);
        }
        db.SaveChanges();
    }
}