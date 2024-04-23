using CloudHotel.Application.Reservations.SearchReservation;

namespace CloudHotel.Application.Reservations.GetReservation;

internal sealed class GetReservationHandler : IRequestHandler<GetReservationByIdQuery, GetReservationResponse?>
{
    private readonly IReservationRepository _reservationRepository;

    public GetReservationHandler(IReservationRepository reservationRepository) =>
        _reservationRepository = reservationRepository;

    public async Task<GetReservationResponse?> Handle(GetReservationByIdQuery query, CancellationToken cancellationToken)
    {
        var reservation = await _reservationRepository.Get(query.Id);

        if (reservation is null)
            return null;

        return GetReservationResponse.Create(reservation);
    }
}