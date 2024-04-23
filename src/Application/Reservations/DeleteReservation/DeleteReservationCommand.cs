namespace CloudHotel.Application.Reservations.DeleteReservation;

public record struct DeleteReservationCommand(Guid Id) : IRequest<Result<bool, Error>>;
