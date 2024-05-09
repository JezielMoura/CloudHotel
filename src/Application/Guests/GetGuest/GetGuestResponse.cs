using CloudHotel.Domain.GuestAggregate;

namespace CloudHotel.Application.Guests.GetGuest;

public sealed record GetGuestResponse (
    Guid Id, 
    string Name,
    string Email,
    string Phone,
    string DocumentNumber,
    string DocumentType,
    string PostalCode,
    string Street,
    string City,
    string State,
    string Country)
{
    public static GetGuestResponse Create(Guest guest) =>
        new (
            guest.Id, 
            guest.Name, 
            guest.Email, 
            guest.Phone, 
            guest.Document.Number, 
            guest.Document.Type, 
            guest.Address.PostalCode,
            guest.Address.Street,
            guest.Address.City,
            guest.Address.State,
            guest.Address.Country);
}