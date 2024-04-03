namespace Calendar;

class Solution12
{
    static void Enter(CalendarDb db)
    {
        CalendarLogic calendarLogic = new CalendarLogic();

        Console.WriteLine("Use of the command");
        Console.WriteLine(" \"check\" to check is year leap ");
        Console.WriteLine(" \"calc\" to calc interval length");
        Console.WriteLine(" \"day\" to get the name of day of week");
        Console.WriteLine(" \"fetch\" to take information from database ");
        Console.WriteLine(" \"remove\" to remove information from database");
        Console.WriteLine(" \"quit\" to exit");
        Console.WriteLine("");
        string com = Convert.ToString(Console.ReadLine());
        Db_work dbwork = new Db_work(db);
        Request re = new Request();
        switch (com)
        {
            case "check":
                Check(dbwork, re, calendarLogic);
                break;
            case "calc":
                Calc(dbwork, re, calendarLogic);
                break;
            case "day":
                Day(dbwork, re, calendarLogic);
                break;
            case "fetch":
                re.Fetch(dbwork);
                break;
            case "remove":
                re.RemoveOp(dbwork);
                break;
            case "quit":
                return;
            default:
                Console.WriteLine("Wrong comand");
                break;
        }
        Console.WriteLine("");
        Enter(db);
        return;
    }

    static void Check(Db_work dbwork, Request re, CalendarLogic calendarLogic)
    {
        Console.WriteLine("Enter a year:");
        int year = Convert.ToInt32(Console.ReadLine());

        bool ans = calendarLogic.IsLeapYear(year);
        Console.WriteLine(ans ? "Year " + year + " is leap" : "Year " + year + " is NOT leap");

        int s = re.SaveOp();
        re.Save(dbwork, ans, year, s);
    }

    static void Calc(Db_work dbwork, Request re, CalendarLogic calendarLogic)
    {
        Console.WriteLine("Enter dates in this format (yyyy-mm-dd)");
        DateTime date1 = DateTime.Parse(Console.ReadLine());
        DateTime date2 = DateTime.Parse(Console.ReadLine());

        int n = calendarLogic.CalculateDaysBetween(date1, date2);
        Console.WriteLine("The interval between this dates is " + (n == 1 ? n + " day" : n + " days"));

        int s = re.SaveOp();
        re.Save(dbwork, date1, date2, n, s);
    }

    static void Day(Db_work dbwork, Request re, CalendarLogic calendarLogic)
    {
        Console.WriteLine("Enter dates in this format (yyyy-mm-dd)");
        DateTime date = DateTime.Parse(Console.ReadLine());

        DayOfWeek w = calendarLogic.GetWeekDay(date);
        Console.WriteLine("The day of the week " + w);

        int s = re.SaveOp();
        re.Save(dbwork, date, w, s);
    }

    static void Main()
    {
        using var db = new CalendarDb();
        Enter(db);
    }
}

