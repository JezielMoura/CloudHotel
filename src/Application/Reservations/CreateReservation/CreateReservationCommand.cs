using CloudHotel.Domain.GuestAggregate;

namespace CloudHotel.Application.Reservations.CreateReservation;

public sealed record CreateReservationCommand(
    DateOnly Arrival, 
    DateOnly Departure,
    decimal Price,
    string RoomDetails,
    string GuestName,
    string GuestEmail,
    string GuestPhone,
    string GuestDocumentNumber,
    string GuestDocumentType) : IRequest<Result<Guid, Error>>
{
    public Reservation MapToReservation() =>
        new(Guid.NewGuid(), Arrival, Departure, Price, new(Guid.Parse(GetRoomDetailsField(0)), GetRoomDetailsField(1)));

    public Guest MapToGuest() =>
        new(Guid.NewGuid(), GuestName, GuestEmail, GuestPhone, new(GuestDocumentType, GuestDocumentNumber), Address.Empty);

    public string GetRoomDetailsField(int index) =>
        RoomDetails.Split(';')[index];
}
