namespace Calendar;

public class CalendarLogic
{
    public bool IsLeapYear(int year)
    {
        return (year % 400 == 0) || ((year % 4 == 0) && (year % 100 != 0));
    }

    public int CalculateDaysBetween(DateTime startDate, DateTime endDate)
    {
        return Math.Abs(startDate.Subtract(endDate).Days);
    }

    public DayOfWeek GetWeekDay(DateTime date)
    {
        return date.DayOfWeek;
    }
}