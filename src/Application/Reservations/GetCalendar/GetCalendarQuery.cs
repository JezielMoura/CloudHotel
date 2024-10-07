namespace CloudHotel.Application.Reservations.GetCalendar;

public record GetCalendarQuery(string? From, string? To) : IRequest<IEnumerable<GetCalendarResponse>>
{
    public DateOnly GetFrom() => Parse(From, -2);
    public DateOnly GetTo() => Parse(To, 58);

    public static DateOnly Parse(string? value, int incrementDays) => 
        !string.IsNullOrWhiteSpace(value) ? DateOnly.Parse(value) : DateOnly.FromDateTime(TimeProvider.System.GetUtcNow().AddDays(incrementDays).Date);
}
