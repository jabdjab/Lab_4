using System.Text.Json;
using System.Xml.Serialization;

namespace Calendar;

public class Request
{
    public Request() { }

    public int SaveOp()
    {
        Console.WriteLine("Use of the command");
        Console.WriteLine("write \"SaveToDb\" to save information to database ");
        Console.WriteLine("write \"SaveJSON\" to save information JSON ");
        Console.WriteLine("write \"SaveXML\" to save information XML ");
        Console.WriteLine("write \"Skip\" to come back to the menu ");
        string com = Convert.ToString(Console.ReadLine());
        switch (com)
        {
            case "SaveToDb":
                return 1;
            case "SaveJSON":
                return 2;
            case "SaveXML":
                return 3;
            case "Skip":
                return 0;
            default:
                Console.WriteLine("Wrong command");
                return SaveOp();
        }
    }

    public void Save(Db_work dbwork, bool ans, int year, int s)
    {
        switch (s)
        {
            case 1:
                dbwork.Create(ans, year);
                Console.WriteLine("Saved to Database");
                return;
            case 2:
                var obj = new Leapnes { leap = ans, year = year };
                string jsonString = JsonSerializer.Serialize(obj);
                File.WriteAllText("leap_year.json", jsonString);
                Console.WriteLine("Saved in JSON format");
                return;
            case 3:
                var obx = new Leapnes { leap = ans, year = year };
                var serializer = new XmlSerializer(typeof(Leapnes));
                using (var writer = new StreamWriter("output.xml"))
                    serializer.Serialize(writer, obx);
                Console.WriteLine("Saved in XML format");
                return;
            default:
                return;
        }
    }

    public void Save(Db_work dbwork, DateTime date1, DateTime date2, int n, int s)
    {
        switch (s)
        {
            case 1:
                dbwork.Create(date1, date2, n);
                Console.WriteLine("Saved to Database");
                return;
            case 2:
                var obj = new Interval { date1 = date1, date2 = date2, interval = n };
                string jsonString = JsonSerializer.Serialize(obj);
                File.WriteAllText("leap_year.json", jsonString);
                Console.WriteLine("Saved in JSON format");
                return;
            case 3:
                var obx = new Interval { date1 = date1, date2 = date2, interval = n };
                var serializer = new XmlSerializer(typeof(Interval));
                using (var writer = new StreamWriter("output.xml"))
                    serializer.Serialize(writer, obx);
                Console.WriteLine("Saved in XML format");
                return;
            default:
                return;
        }
    }
    public void Save(Db_work dbwork, DateTime date, DayOfWeek w, int s)
    {
        switch (s)
        {
            case 1:
                dbwork.Create(date, w == DayOfWeek.Sunday ? 7 : (int)w);
                Console.WriteLine("Saved to Database");
                return;
            case 2:
                var obj = new DayWeek { date = date, dayWeek = w == DayOfWeek.Sunday ? 7 : (int)w };
                string jsonString = JsonSerializer.Serialize(obj);
                File.WriteAllText("leap_year.json", jsonString);
                Console.WriteLine("Saved in JSON format");
                return;
            case 3:
                var obx = new DayWeek { date = date, dayWeek = w == DayOfWeek.Sunday ? 7 : (int)w };
                var serializer = new XmlSerializer(typeof(DayWeek));
                using (var writer = new StreamWriter("output.xml"))
                    serializer.Serialize(writer, obx);
                Console.WriteLine("Saved in XML format");
                return;
        }
    }

    public void Fetch(Db_work db_work)
    {
        Console.WriteLine("Use of the command");
        Console.WriteLine(" \"Leap\" to upload inforation about leap of year ");
        Console.WriteLine(" \"Interval\" to uplad information about interval between to dates");
        Console.WriteLine(" \"DayWeek\" to upload information about the name of day of week by date");
        Console.WriteLine("write \"Skip\" to come back to the menu ");

        string com = Convert.ToString(Console.ReadLine());

        if (com == "Leap")
        {
            Console.WriteLine("Enter a year:");
            int year = Convert.ToInt32(Console.ReadLine());
            bool? leapness = db_work.Read(year);
            Console.WriteLine(leapness.HasValue ? leapness : "doesn't have information");
        }
        if (com == "Interval")
        {
            Console.WriteLine("Enter dates in this format (yyyy-mm-dd):");
            DateTime datest = DateTime.Parse(Console.ReadLine());
            DateTime dateen = DateTime.Parse(Console.ReadLine());
            int? interval = db_work.Read(datest, dateen);
            Console.WriteLine(interval.HasValue ? interval : "doesn't have information");
        }
        if (com == "DayWeek")
        {
            Console.WriteLine("Enter date in this format (yyyy-mm-dd)");
            DateTime date = DateTime.Parse(Console.ReadLine());
            int? dayWeek = db_work.Read(date);
            Console.WriteLine(dayWeek.HasValue ? dayWeek : "doesn't have information");
        }
    }

    public void RemoveOp(Db_work dbwork)
    {
        Console.WriteLine("Use of the command");
        Console.WriteLine(" \"Leap\" to upload inforation about leap of year ");
        Console.WriteLine(" \"Interval\" to uplad information about interval between to dates");
        Console.WriteLine(" \"DayWeek\" to upload information about the name of day of week by date");
        Console.WriteLine("write \"Skip\" to come back to the menu ");

        string com = Convert.ToString(Console.ReadLine());

        if (com == "Leap")
        {
            Console.WriteLine("Enter a year: ");
            int n = Convert.ToInt32(Console.ReadLine());
            dbwork.Remove(n);

        }
        if (com == "Interval")
        {
            Console.WriteLine("Enter dates in this format (yyyy-mm-dd):");
            DateTime datest = DateTime.Parse(Console.ReadLine());
            DateTime dateen = DateTime.Parse(Console.ReadLine());
            dbwork.Remove(datest, dateen);
        }
        if (com == "DayWeek")
        {
            Console.WriteLine("Enter date in this format (yyyy-mm-dd)");
            DateTime date = DateTime.Parse(Console.ReadLine());
            dbwork.Remove(date);
        }
    }
}