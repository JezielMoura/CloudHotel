namespace CloudHotel.Application.Reservations.UpdateReservation;

internal sealed class UpdateReservationHandler : IRequestHandler<UpdateReservationCommand, Result<bool, Error>>
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateReservationHandler(IReservationRepository reservationRepository, IUnitOfWork unitOfWork) =>
        (_reservationRepository, _unitOfWork) = (reservationRepository, unitOfWork);

    public async Task<Result<bool, Error>> Handle(UpdateReservationCommand command, CancellationToken cancellationToken)
    {
        var reservation = command.MapToReservation();
        var reservationCountOnRange = await _reservationRepository.Count(reservation.Id, reservation.Room.Id, command.Arrival, command.Departure);

        reservation.SetGuestDetails(command.GuestId, command.GuestName);
        reservation.SetStatus(ReservationStatus.FromNumber(command.Status));

        if (reservationCountOnRange > 0)
            return new Error(Errors: [new ("A data e acomodação selecionada não está disponível")]);

        await _reservationRepository.Update(reservation);

        return await _unitOfWork.Commit();
    }
}