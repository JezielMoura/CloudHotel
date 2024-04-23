using CloudHotel.Domain.GuestAggregate;

namespace CloudHotel.Application.Guests.CreateGuest;

public sealed record CreateGuestCommand(
    string Name, 
    string Email,
    string Phone,
    string DocumentType,
    string DocumentNumber,
    string AddressStreet,
    string AddressPostalCode,
    string AddressCity,
    string AddressState,
    string AddressCountry) : IRequest<Result<Guid, Error>>
{
    public Guest MapToGuest() =>
        new(
            Guid.NewGuid(), 
            Name,
            Email,
            Phone,
            new(DocumentNumber, DocumentType),
            new(AddressStreet, AddressPostalCode, AddressCity, AddressState, AddressCountry));
}