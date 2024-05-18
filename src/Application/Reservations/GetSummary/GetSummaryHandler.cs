using CloudHotel.Application.Reservations.SearchReservation;

namespace CloudHotel.Application.Reservations.GetReservation;

internal sealed class GetSummaryHandler (IAppDbContext dbContext) : IRequestHandler<GetSummaryQuery, GetSummaryResponse>
{
    public async Task<GetSummaryResponse> Handle(GetSummaryQuery query, CancellationToken ct)
    {
        var today = DateOnly.FromDateTime(DateTime.Now);
        var todayArrival = await dbContext.Reservations.Where(x => x.Arrival == today).CountAsync(ct);
        var todayDeparture = await dbContext.Reservations.Where(x => x.Departure == today).CountAsync(ct);
        var todayInHouse = await dbContext.Reservations.Where(x => x.Arrival <= today && x.Departure > today).CountAsync(ct);
        //var a = DateTime.UtcNow.AddHours(-3).Date.Day;
        //var teste = await dbContext.Reservations.Select(x => x.CreatedOn.ToUniversalTime().AddHours(-3).Date.Day).ToListAsync(ct);
        var todayReservations = await dbContext.Reservations.Where(x => x.CreatedOn.ToUniversalTime().AddHours(-3).Date.Day == DateTime.UtcNow.AddHours(-3).Date.Day).ToListAsync(ct);

        return GetSummaryResponse.Create(todayArrival, todayDeparture, todayInHouse, todayReservations);
    }
}