using CloudHotel.Domain.GuestAggregate;

namespace CloudHotel.Application.Reservations.CreateReservation;

public sealed record CreateReservationCommand(
    DateOnly Arrival, 
    DateOnly Departure,
    decimal Price,
    string RoomId,
    string RoomCode,
    string GuestName,
    string GuestEmail,
    string GuestPhone,
    string GuestDocumentNumber,
    string GuestDocumentType,
    string AddressStreet,
    string AddressPostalCode,
    string AddressCity,
    string AddressState,
    string AddressCountry) : IRequest<Result<Guid, Error>>
{
    public Reservation MapToReservation() =>
        new(Guid.NewGuid(), Arrival, Departure, Price, new(Guid.Parse(RoomId), RoomCode));

    public Guest MapToGuest() =>
        new(Guid.NewGuid(), GuestName, GuestEmail, GuestPhone, new(GuestDocumentNumber, GuestDocumentType), MapToAddress());

    private Address MapToAddress() =>
        new Address(AddressStreet, AddressPostalCode, AddressCity, AddressState, AddressCountry);
}
