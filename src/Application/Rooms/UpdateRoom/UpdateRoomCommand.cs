namespace CloudHotel.Application.Rooms.UpdateRoom;

public sealed record UpdateRoomCommand(
    Guid Id,
    string Name, 
    string Description, 
    string Code, 
    IEnumerable<string>? Photos = null) : IRequest<Result<bool, Error>>
{
    public Room MapToRoom() =>
        new (Id, Name, Description, Code, Photos?.ToList());
}