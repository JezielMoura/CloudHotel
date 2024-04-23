
namespace CloudHotel.Application.Reservations.SearchReservation;

public sealed record SearchReservationQuery(
    string? ArrivalFrom = null, 
    string? ArrivalTo = null) : IRequest<IEnumerable<SearchReservationResponse>>
{
    public DateOnly GetArrivalFrom() => Parse(ArrivalFrom, incrementDays: 0);
    public DateOnly GetArrivalTo() => Parse(ArrivalTo, incrementDays: 30);

    public static DateOnly Parse(string? value, int incrementDays) => 
        !string.IsNullOrWhiteSpace(value) ? DateOnly.Parse(value) : DateOnly.FromDateTime(TimeProvider.System.GetUtcNow().AddDays(incrementDays).Date);
}

