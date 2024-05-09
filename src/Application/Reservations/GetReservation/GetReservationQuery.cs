namespace CloudHotel.Application.Reservations.GetReservation;

public record struct GetReservationByIdQuery (Guid Id) : IRequest<GetReservationResponse?>;
