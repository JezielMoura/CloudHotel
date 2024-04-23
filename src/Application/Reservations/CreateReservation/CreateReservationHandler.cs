using CloudHotel.Domain.GuestAggregate;

namespace CloudHotel.Application.Reservations.CreateReservation;

internal sealed class CreateReservationHandler : IRequestHandler<CreateReservationCommand, Result<Guid, Error>>
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IGuestRepository _guestRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateReservationHandler(IReservationRepository reservationRepository, IUnitOfWork unitOfWork, IGuestRepository guestRepository)
    {
        _reservationRepository = reservationRepository; 
        _guestRepository = guestRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid, Error>> Handle(CreateReservationCommand command, CancellationToken cancellationToken)
    {
        var guest = command.MapToGuest();
        var reservation = command.MapToReservation();

        reservation.SetGuestDetails(guest.Id, guest.Name);
        reservation.SetCreatedOnWithCurrentDate();

        await _guestRepository.Add(guest);
        await _reservationRepository.Add(reservation);
        
        return await _unitOfWork.Commit(reservation.Id);
    }
}