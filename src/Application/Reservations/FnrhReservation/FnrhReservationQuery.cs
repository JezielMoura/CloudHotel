namespace CloudHotel.Application.Reservations.FnrhReservation;

public record struct FnrhReservationQuery (Guid Id) : IRequest<string?>;
