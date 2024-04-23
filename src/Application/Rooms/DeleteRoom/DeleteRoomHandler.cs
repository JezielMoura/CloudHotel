namespace CloudHotel.Application.Rooms.DeleteRoom;

internal sealed class DeleteRoomHandler : IRequestHandler<DeleteRoomCommand, Result<bool, Error>>
{
    private readonly IRoomRepository _roomRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteRoomHandler(IRoomRepository roomRepository, IUnitOfWork unitOfWork) =>
        (_roomRepository, _unitOfWork) = (roomRepository, unitOfWork);

    public async Task<Result<bool, Error>> Handle(DeleteRoomCommand command, CancellationToken cancellationToken)
    {
        await _roomRepository.Delete(command.Id);
        return await _unitOfWork.Commit();
    }
}