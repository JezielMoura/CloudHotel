namespace CloudHotel.Application.Reservations.SearchReservation;

public sealed record SearchReservationResponse(
    Guid Id,
    DateOnly Arrival,
    DateOnly Departure,
    decimal Price,
    Guid RoomId,
    string RoomCode,
    Guid GuestId,
    string GuestName,
    int TotalNights
)
{
    public static SearchReservationResponse Create(Reservation reservation) =>
        new(
            reservation.Id, 
            reservation.Arrival, 
            reservation.Departure, 
            reservation.Price, 
            reservation.Room.Id, 
            reservation.Room.Code,
            reservation.Guest.Id,
            reservation.Guest.Name,
            reservation.GetNightsNumber());
}