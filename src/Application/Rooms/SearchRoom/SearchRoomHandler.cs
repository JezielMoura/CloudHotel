using System.Text.RegularExpressions;

namespace CloudHotel.Application.Rooms.SearchRoom;

internal sealed class SearchRoomHandler(IAppDbContext appDbContext) : IRequestHandler<SearchRoomQuery, IEnumerable<SearchRoomGroupResponse>>
{
    public async Task<IEnumerable<SearchRoomGroupResponse>> Handle(SearchRoomQuery query, CancellationToken cancellationToken)
    {
        var rooms = await appDbContext.Rooms
            .OrderBy(x => x.Name)
            .ThenBy(x => x.Code)
            .ToListAsync(cancellationToken);

        var roomGroups = rooms.GroupBy(g => g.Name);
       
        return roomGroups.Select(SearchRoomGroupResponse.Create);
    }
}
