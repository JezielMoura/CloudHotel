namespace CloudHotel.Application.Guests.GetGuest;

public record GetGuestQuery(Guid Id) : IRequest<Result<GetGuestResponse, Error>>;
