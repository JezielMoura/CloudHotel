
namespace CloudHotel.Application.Guests.SearchGuest;

internal sealed class SearchGuestsHandler(IAppDbContext appDbContext) : IRequestHandler<SearchGuestQuery, IEnumerable<SearchGuestResponse>>
{
    private readonly IAppDbContext _appDbContext = appDbContext;

    public async Task<IEnumerable<SearchGuestResponse>> Handle(SearchGuestQuery query, CancellationToken cancellationToken)
    {
        var results = await _appDbContext.Guests
            .OrderBy(o => o.Name)
            .Skip(query.Offset)
            .Take(query.Limit)
            .Select(s => new SearchGuestResponse(s.Id, s.Name, s.Email, s.Phone))
            .ToListAsync(cancellationToken);

        return results;
    }
}
