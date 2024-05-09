using CloudHotel.Domain.GuestAggregate;

namespace CloudHotel.Application.Reservations.GetReservation;

internal sealed class GetReservationHandler : IRequestHandler<GetReservationByIdQuery, GetReservationResponse?>
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IGuestRepository _guestRepository;

    public GetReservationHandler(IReservationRepository reservationRepository, IGuestRepository guestRepository)
    {
        _reservationRepository = reservationRepository;
        _guestRepository = guestRepository;
    }

    public async Task<GetReservationResponse?> Handle(GetReservationByIdQuery query, CancellationToken cancellationToken)
    {
        var reservation = await _reservationRepository.Get(query.Id);

        if (reservation is null)
            return null;

        var guest = await _guestRepository.Get(reservation.Guest.Id);

        return GetReservationResponse.Create(reservation, guest!);
    }
}
