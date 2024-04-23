using CloudHotel.Application.Reservations.SearchReservation;

namespace CloudHotel.Application.Reservations.GetReservation;

public record struct GetSummaryQuery() : IRequest<GetSummaryResponse>;
