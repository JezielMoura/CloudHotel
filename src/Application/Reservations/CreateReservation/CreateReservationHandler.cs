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
        var reservationCountOnRange = await _reservationRepository.Count(reservation.Id, reservation.Room.Id, command.Arrival, command.Departure);

        if (reservationCountOnRange > 0)
            return new Error(Errors: [new ("A data e acomodação selecionada não está disponível")]);

        reservation.SetGuestDetails(guest.Id, guest.Name);
        reservation.SetCreatedOnWithCurrentDate();

        await _guestRepository.Add(guest);
        await _reservationRepository.Add(reservation);
        
        return await _unitOfWork.Commit(reservation.Id);
    }
}