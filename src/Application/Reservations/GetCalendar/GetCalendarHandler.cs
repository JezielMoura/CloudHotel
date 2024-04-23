namespace CloudHotel.Application.Reservations.GetCalendar;

internal sealed class GetCalendarHandler(IAppDbContext appDbContext) : IRequestHandler<GetCalendarQuery, IEnumerable<GetCalendarResponse>>
{
    public async Task<IEnumerable<GetCalendarResponse>> Handle(GetCalendarQuery query, CancellationToken ct)
    {
        var dateRage = new DateRange(query.GetFrom(), query.GetTo());
        var arrival = query.GetFrom().AddDays(-7);
        var departure = query.GetTo().AddDays(7);
        var reservations = await appDbContext.Reservations.Where(x => x.Arrival >= arrival && x.Departure <= departure).ToListAsync(ct);
        var roomGroups = await appDbContext.Rooms.GroupBy(x => x.Name).ToListAsync(ct);

        return roomGroups.Select(roomGroup => GetCalendarResponse.Create(roomGroup, reservations, dateRage)).ToList();
    }
}
