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

        await _reservationRepository.Update(reservation);

        return await _unitOfWork.Commit();
    }
}