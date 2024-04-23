using CloudHotel.Domain.GuestAggregate;

namespace CloudHotel.Application.Guests.CreateGuest;

internal sealed class CreateGuestHandler : IRequestHandler<CreateGuestCommand, Result<Guid, Error>>
{
    private readonly IGuestRepository _guestRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateGuestHandler(IGuestRepository guestRepository, IUnitOfWork unitOfWork) =>
        (_guestRepository, _unitOfWork) = (guestRepository, unitOfWork);

    public async Task<Result<Guid, Error>> Handle(CreateGuestCommand command, CancellationToken cancellationToken)
    {
        var guest = command.MapToGuest();

        await _guestRepository.Add(guest);

        return await _unitOfWork.Commit(guest.Id);
    }
}
