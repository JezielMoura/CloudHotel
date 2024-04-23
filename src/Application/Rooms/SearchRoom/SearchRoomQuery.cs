namespace CloudHotel.Application.Rooms.SearchRoom;

public class SearchRoomQuery(int page = 1, int limit = 10) : ListQuery, IRequest<IEnumerable<SearchRoomGroupResponse>>
{
    public override int Page => page;
    public override int Limit => limit;
}
