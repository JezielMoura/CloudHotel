
namespace CloudHotel.Application.Reservations.SearchReservation;

internal sealed class SearchReservationHandler : IRequestHandler<SearchReservationQuery, IEnumerable<SearchReservationResponse>>
{
    private readonly IAppDbContext _appDbContext;

    public SearchReservationHandler(IAppDbContext appDbContext) =>
        _appDbContext = appDbContext;

    public async Task<IEnumerable<SearchReservationResponse>> Handle(SearchReservationQuery query, CancellationToken cancellationToken)
    {
        var results = await _appDbContext.Reservations
            .Where(x => x.Arrival >= query.GetArrivalFrom() && x.Arrival <= query.GetArrivalTo())
            .ToListAsync(cancellationToken);

        return results.Select(SearchReservationResponse.Create);
    }
}
