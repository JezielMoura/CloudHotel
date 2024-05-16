using CloudHotel.Domain.GuestAggregate;

namespace CloudHotel.Application.Reservations.UpdateReservation;

public sealed record UpdateReservationCommand(
    Guid Id,
    DateOnly Arrival, 
    DateOnly Departure,
    decimal Price,
    int Status,
    Guid RoomId,
    string RoomCode,
    Guid GuestId,
    string GuestName,
    string GuestEmail,
    string GuestPhone,
    string GuestDocumentNumber,
    string GuestDocumentType,
    string AddressStreet,
    string AddressPostalCode,
    string AddressCity,
    string AddressState,
    string AddressCountry,
    DateTime CreatedOn) : IRequest<Result<bool, Error>>
{
    public Reservation MapToReservation() =>
        new (Id, Arrival, Departure, Price, new(RoomId, RoomCode),CreatedOn);

    public Guest MapToGuest() =>
        new (GuestId, GuestName, GuestEmail, GuestPhone, new(GuestDocumentType, GuestDocumentNumber), MapToAddress());

    private Address MapToAddress() =>
        new (AddressStreet, AddressPostalCode, AddressCity, AddressState, AddressCountry);
}
