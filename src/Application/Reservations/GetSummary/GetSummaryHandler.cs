using CloudHotel.Application.Reservations.SearchReservation;

namespace CloudHotel.Application.Reservations.GetReservation;

internal sealed class GetSummaryHandler (IAppDbContext dbContext) : IRequestHandler<GetSummaryQuery, GetSummaryResponse>
{
    public async Task<GetSummaryResponse> Handle(GetSummaryQuery query, CancellationToken ct)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var todayArrival = await dbContext.Reservations.Where(x => x.Arrival == today).CountAsync(ct);
        var todayDeparture = await dbContext.Reservations.Where(x => x.Departure == today).CountAsync(ct);
        var todayInHouse = await dbContext.Reservations.Where(x => x.Arrival <= today && x.Departure > today).CountAsync(ct);
        var todayReservations = await dbContext.Reservations.Where(x => x.CreatedOn.Date == DateTime.UtcNow.AddHours(-3).Date).ToListAsync(ct);

        return GetSummaryResponse.Create(todayArrival, todayDeparture, todayInHouse, todayReservations);
    }
}