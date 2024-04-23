namespace CloudHotel.Application.Guests.SearchGuest;

public sealed record SearchGuestResponse(
    Guid Id,
    string Name,
    string Email,
    string Phone
);