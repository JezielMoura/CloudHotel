namespace CloudHotel.Application.Reservations.SearchReservation;

public sealed record GetSummaryResponse(
    int Arrival,
    int Departure,
    int InHouse,
    IEnumerable<ReservationSummary> Reservations
)
{
    public static GetSummaryResponse Create(int arrival, int departure, int inHouse, IEnumerable<Reservation> reservations) =>
        new( arrival, departure, inHouse, reservations.Select(ReservationSummary.Create));
}

public sealed record ReservationSummary (DateOnly Arrival, DateOnly Departure, string GuestName, decimal Price)
{
    public static ReservationSummary Create(Reservation reservation) =>
        new (reservation.Arrival, reservation.Departure, reservation.Guest.Name, reservation.Price);
}