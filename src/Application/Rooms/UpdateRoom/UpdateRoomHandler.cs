namespace CloudHotel.Application.Rooms.UpdateRoom;

internal sealed class UpdateRoomHandler : IRequestHandler<UpdateRoomCommand, Result<bool, Error>>
{
    private readonly IRoomRepository _roomRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateRoomHandler(IRoomRepository roomRepository, IUnitOfWork unitOfWork) =>
        (_roomRepository, _unitOfWork) = (roomRepository, unitOfWork);

    public async Task<Result<bool, Error>> Handle(UpdateRoomCommand command, CancellationToken cancellationToken)
    {
        var room = command.MapToRoom();
        await _roomRepository.Update(room);
        
        return await _unitOfWork.Commit();
    }
}
