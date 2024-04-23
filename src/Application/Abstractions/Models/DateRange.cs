namespace CloudHotel.Application.Abstractions.Models;

public readonly struct DateRange
{
    public DateOnly From { get; }
    public DateOnly To { get; }
    public readonly int Days => To.DayNumber - From.DayNumber + 1;
    public readonly IList<DateOnly> Dates => Enumerable.Range(0, Days).Select(From.AddDays).ToList();

    public DateRange(DateOnly from, DateOnly to) : this() =>
        (From, To) = (from, to);
}
