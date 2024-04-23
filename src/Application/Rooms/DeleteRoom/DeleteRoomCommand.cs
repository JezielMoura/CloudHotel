
namespace CloudHotel.Application.Rooms.DeleteRoom;

public record struct DeleteRoomCommand(Guid Id) : IRequest<Result<bool, Error>>;
