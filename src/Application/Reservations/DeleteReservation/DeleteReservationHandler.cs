namespace CloudHotel.Application.Reservations.DeleteReservation;

internal sealed class DeleteReservationHandler : IRequestHandler<DeleteReservationCommand, Result<bool, Error>>
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteReservationHandler(IReservationRepository reservationRepository, IUnitOfWork unitOfWork) =>
        (_reservationRepository, _unitOfWork) = (reservationRepository, unitOfWork);

    public async Task<Result<bool, Error>> Handle(DeleteReservationCommand command, CancellationToken cancellationToken)
    {
        await _reservationRepository.Delete(command.Id);
        return await _unitOfWork.Commit();
    }
}