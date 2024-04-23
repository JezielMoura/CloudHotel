namespace CloudHotel.Application.Rooms.CreateRoom;

public sealed record CreateRoomCommand(
    string Name, 
    string Description,
    string Code,
    IEnumerable<string>? Photos = null,
    int Quantity = 1) : IRequest<Result<bool, Error>>
{
    public Room MapToRoom(int index) =>
        new(Guid.NewGuid(), Name, Description, $"{Code} ({index})", Photos?.ToList());
}