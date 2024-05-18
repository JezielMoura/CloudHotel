using System.Text.RegularExpressions;

namespace CloudHotel.Application.Rooms.SearchRoom;

public sealed record SearchRoomGroupResponse(string Name, IEnumerable<SearchRoomResponse> Rooms)
{
    public static SearchRoomGroupResponse Create(IGrouping<string, Room> group) =>
        new SearchRoomGroupResponse(group.Key, group.Select(SearchRoomResponse.Create).OrderBy(x => int.Parse(Regex.Replace(x.Code, @"\D", ""))));
}

public sealed record SearchRoomResponse (Guid Id, string Name, string Description, string Code, IEnumerable<string>? Photos)
{
    public static SearchRoomResponse Create(Room room) =>
        new(room.Id, room.Name, room.Description, room.Code, room.Photos);
}