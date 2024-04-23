using CloudHotel.Domain.GuestAggregate;

namespace CloudHotel.Application.Reservations.UpdateReservation;

public sealed record UpdateReservationCommand(
    Guid Id,
    DateOnly Arrival, 
    DateOnly Departure,
    decimal Price,
    Guid RoomId,
    string RoomCode,
    Guid GuestId,
    string GuestName,
    string GuestEmail,
    string GuestPhone,
    string GuestDocumentNumber,
    string GuestDocumentType) : IRequest<Result<bool, Error>>
{
    public Reservation MapToReservation()
    {
        var reservation = new Reservation(Id, Arrival, Departure, Price, new(RoomId, RoomCode));
        reservation.SetGuestDetails(GuestId, GuestName);
        return reservation;
    }

    public Guest MapToGuest() =>
        new(GuestId, GuestName, GuestEmail, GuestPhone, new(GuestDocumentType, GuestDocumentNumber), Address.Empty);
}
