using Microsoft.EntityFrameworkCore;
using Calendar;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<CalendarDb>(options =>
    options.UseSqlite(connectionString));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/leap_year", (CalendarDb context, int year) =>
{
    Db_work db_Work = new Db_work(context);
    bool? leap = db_Work.Read(year);

    if (leap.HasValue)
    {
        return Results.Ok(new { leap });
    }
    else
    {
        return Results.BadRequest(new { error = "Year wasn't found in db" });
    }
});

app.MapGet("/leap_year/{id}", (CalendarDb context, int id) =>
{
    Leapnes? leapnes = context.Leapness.FirstOrDefault(a => a.Leapnes_id == id);
    return Results.Ok(new { leapnes });
});

app.MapGet("/all_leap", (CalendarDb context) =>
{
    return Results.Ok(context.Leapness.ToList());
});

app.MapPost("/leap_year", (CalendarDb context, LeapnesRequest request) =>
{
    Db_work db_Work = new Db_work(context);
    bool leap = new CalendarLogic().IsLeapYear(request.year);
    int id = db_Work.Create(leap, request.year);
    return Results.Created($"/leap_year/{id}", new { id, request, leap });
});

app.MapGet("/interval", (CalendarDb context, DateTime startDate, DateTime endDate) =>
{
    Db_work db_Work = new Db_work(context);
    int? interval = db_Work.Read(startDate, endDate);

    if (interval.HasValue)
    {
        return Results.Ok(new { interval });
    }
    else
    {
        return Results.NotFound();
    }
});

app.MapGet("/interval/{id}", (CalendarDb context, int id) =>
{
    Interval? interval = context.Intervals.FirstOrDefault(a => a.interval_id == id);

    return Results.Ok(new { interval });
});

app.MapGet("/all_interval", (CalendarDb context) =>
{
    return Results.Ok(context.Intervals.ToList());
});

app.MapPost("/interval", (CalendarDb context, IntervalRequest request) =>
{
    Db_work db_Work = new Db_work(context);
    int interval = new CalendarLogic().CalculateDaysBetween(request.date1, request.date2);
    int id = db_Work.Create(request.date1, request.date2, interval);
    return Results.Created($"/interval/{id}", new { id, request, interval });
});

app.MapGet("/day_week", (CalendarDb context, DateTime date) =>
{
    Db_work db_Work = new Db_work(context);
    int? dayWeek = db_Work.Read(date);

    if (dayWeek.HasValue)
    {
        return Results.Ok(new { dayWeek });
    }
    else
    {
        return Results.NotFound();
    }
});

app.MapGet("/day_week/{id}", (CalendarDb context, int id) =>
{
    Interval? interval = context.Intervals.FirstOrDefault(a => a.interval_id == id);

    return Results.Ok(new { interval });
});

app.MapGet("/all_day_week", (CalendarDb context) =>
{
    return Results.Ok(context.Intervals.ToList());
});

app.MapPost("/day_week", (CalendarDb context, DayWeekRequest item) =>
{
    Db_work db_Work = new Db_work(context);
    DayOfWeek dayWeek = new CalendarLogic().GetWeekDay(item.date);
    int id = db_Work.Create(item.date, dayWeek == DayOfWeek.Sunday ? 7 : (int)dayWeek);
    return Results.Created($"/interval/{id}", new { id, item, dayWeek });
});

app.Run();
