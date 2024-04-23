namespace CloudHotel.Application.Rooms.CreateRoom;

internal sealed class CreateRoomHandler : IRequestHandler<CreateRoomCommand, Result<bool, Error>>
{
    private readonly IRoomRepository _roomRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateRoomHandler(IRoomRepository roomRepository, IUnitOfWork unitOfWork) =>
        (_roomRepository, _unitOfWork) = (roomRepository, unitOfWork);

    public async Task<Result<bool, Error>> Handle(CreateRoomCommand command, CancellationToken cancellationToken)
    {
        var roomRange = Enumerable.Range(1, command.Quantity);
        var roomList =  roomRange.Select(x => command.MapToRoom(x));

        await _roomRepository.Add(roomList);

        return await _unitOfWork.Commit();
    }
}
